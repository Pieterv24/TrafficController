import _ from 'lodash'

import {Lane, LightData, LaneId} from '../models'
import UniHelper from '../helpers/UnidiotifyHelper'
import dataOut from './OutgoingDataFactory'

import matrix from '../conflictMatrix.json'
import config from '../config.json'

class CarRouter {
  constructor (socket, store, updateWindow) {
    this.socket = socket
    this.store = store
    this.matrix = matrix
    this.updateWindow = updateWindow
    this.manual = false
  }

  doCycle () {
    if (!this.manual) {
      let changeLightArray = []
      changeLightArray = _.concat(changeLightArray, this.handleOranges(), this.handleCertainReds())
      this.checkCloseBridge()
      let prioritizedRedList = this.generateRedPriorityList()
      prioritizedRedList = _.reverse(_.sortBy(prioritizedRedList, ['score']))
      _.forEach(prioritizedRedList, (light) => {
      // Detect 1.13.0 and ignore it
        if (UniHelper.laneIdToString(light.id) === '1.13.0') {
        // Is handled as continue
          return true
        }
        switch (light.id.typeId) {
          case 4:
          // Do boat stuff
            let boatLights = this.handleBridge(light)
            if (boatLights instanceof Array) {
              changeLightArray = _.concat(changeLightArray, boatLights)
            }
            break
          case 1:
          case 2:
          case 3:
          // Do light stuff
            let handledLights = this.handleLights(light)
            changeLightArray = _.concat(changeLightArray, handledLights)
            break
          default:
          // continue
            return true
        }
      })
      if (changeLightArray.length > 0) {
        let command = dataOut.getTrafficLightsResponse(changeLightArray)
        // console.log(command)
        this.socket.write(command + '\n')
        this.updateWindow()
      }
    }
  }

  handleBridge (light) {
    let bridgeLightIndex = _.findIndex(this.store.Lanes, {id: new LaneId(1, 13, 0)})
    // Handle boats
    if (this.store.Bridge.open && !this.store.Bridge.changing && Date.now() - this.store.Bridge.lastChanged < config.maxBridgeTime) {
      let changeLightArray = []

      let conflictReference = _.find(this.matrix, {id: UniHelper.laneIdToString(light.id)})
      let activeBoatLights = _.filter(this.store.Lanes, l => {
        return ((UniHelper.compareIds(l.id, new LaneId(4, 1, 0)) || UniHelper.compareIds(l.id, new LaneId(4, 2, 0))) && (l.state === 'green' || l.state === 'orange'))
      })
      // console.log(activeBoatLights)
      let conflicts = _.filter(activeBoatLights, gl => {
        return conflictReference.blockedBy.includes(UniHelper.laneIdToString(gl.id))
      })

      if (conflicts.length > 0) {
        _.forEach(conflicts, c => {
          let conflict = this.handleBridgeConflict(c.id)
          if (conflict !== undefined) {
            changeLightArray.push(conflict)
          }
        })
      } else {
        let lightIndex = _.findIndex(this.store.Lanes, {id: light.id})
        if (lightIndex !== -1) {
          this.store.Lanes[lightIndex].state = 'green'
          this.store.Lanes[lightIndex].lastLightChange = Date.now()
          changeLightArray.push(new LightData(light.id, 'green'))
        }
      }

      return changeLightArray
    } else if (Date.now() - this.store.Bridge.lastChanged > config.minBridgeIntervalTime && !this.store.Bridge.changing && this.store.Lanes[bridgeLightIndex].state === 'red') {
      // Open bridge
      let command = dataOut.getBridgeResponse(true)
      this.store.Bridge.changing = true
      this.socket.write(command + '\n')
      // console.log(command)
      this.updateWindow()
    } else if (Date.now() - this.store.Bridge.lastChanged > config.minBridgeIntervalTime && !this.store.Bridge.changing) {
      // Red bridge light
      this.store.Lanes[bridgeLightIndex].state = 'red'
      this.updateWindow()
      return [new LightData(new LaneId(1, 13, 0), 'red')]
    }
  }

  checkCloseBridge () {
    let boatStuff = _.filter(this.store.Lanes, l => {
      return ((UniHelper.compareIds(l.id, '4.1.0') || UniHelper.compareIds(l.id, '4.2.0')))//  &&
      // (((l.primaryTrigger === false && l.secondaryTrigger === false) && (Date.now() - l.lastTriggerChange > config.triggerDeactivate) &&
      // l.state === 'red') || (Date.now() - this.store.Bridge.lastChanged > config.maxBridgeTime)))
    })
    console.log(boatStuff.length)
    if (boatStuff.length === 2 && !this.store.Bridge.changing && this.store.Bridge.open) {
      // Close bridge
      let command = dataOut.getBridgeResponse(false)
      this.store.Bridge.changing = true
      this.socket.write(command + '\n')
      // console.log(command)
      this.updateWindow()
    }
    if (boatStuff.length === 2 && !this.store.Bridge.open && !this.store.Bridge.changing && _.find(this.store.Lanes, {id: new LaneId(1, 13, 0)}).state === 'red') {
      let lightIndex = _.findIndex(this.store.Lanes, {id: new LaneId(1, 13, 0)})
      this.store.Lanes[lightIndex].state = 'green'
      let lightCommand = dataOut.getTrafficLightResponse(new LightData(new LaneId(1, 13, 0), 'green'))
      this.socket.write(lightCommand + '\n')
      this.updateWindow()
    }
  }

  scoreBridgeLight (lane) {
    if (lane instanceof Lane) {
      if (lane.primaryTrigger || lane.secondaryTrigger) {
        let score = 0
        score += lane.primaryTrigger ? 1 : 0
        let redTime = Date.now() - lane.lastLightChange
        let redTimePercentage = (redTime / (config.maxRedTime / 100)) / 100
        score += 2 * redTimePercentage
        score += lane.weight
        return score
      }
    }
  }

  handleBridgeConflict (id) {
    if (id instanceof LaneId) {
      let conflict = _.find(this.store.Lanes, {id: id})
      if (conflict !== undefined) {
        if (conflict.state === 'green' && Date.now() - conflict.lastLightChange > (config.minGreenTime * 2)) {
          let lightIndex = _.findIndex(this.store.Lanes, {id: conflict.id})
          if (lightIndex !== -1) {
            this.store.Lanes[lightIndex].state = 'orange'
            this.store.Lanes[lightIndex].lastLightChange = Date.now()
            return new LightData(conflict.id, 'orange')
          }
        }
      }
    }
  }

  handleLights (light) {
    let changeLightArray = []
    // Get conflict reference from matrix
    let conflictReference = _.find(this.matrix, {id: UniHelper.laneIdToString(light.id)})
    // Get green lights
    let greenLights = this.getActiveList()
    // Get conflicting green lights
    let conflicts = _.filter(greenLights, gl => {
      return conflictReference.blockedBy.includes(UniHelper.laneIdToString(gl.id))
    })

    if (conflicts.length > 0) {
      _.forEach(conflicts, c => {
        let conflict = this.handleConflict(c.id)
        if (conflict !== undefined) {
          changeLightArray.push(conflict)
        }
      })
    } else {
      let lightIndex = _.findIndex(this.store.Lanes, {id: light.id})
      if (lightIndex !== -1) {
        this.store.Lanes[lightIndex].state = 'green'
        this.store.Lanes[lightIndex].lastLightChange = Date.now()
        changeLightArray.push(new LightData(light.id, 'green'))
      }
    }

    return changeLightArray
  }

  handleConflict (id) {
    if (id instanceof LaneId) {
      let conflict = _.find(this.store.Lanes, {id: id})
      if (conflict !== undefined) {
        if (conflict.state === 'green' && Date.now() - conflict.lastLightChange > config.minGreenTime) {
          let lightIndex = _.findIndex(this.store.Lanes, {id: conflict.id})
          if (lightIndex !== -1) {
            this.store.Lanes[lightIndex].state = 'orange'
            this.store.Lanes[lightIndex].lastLightChange = Date.now()
            return new LightData(conflict.id, 'orange')
          }
        }
      }
    }
  }

  handleOranges () {
    let orangeList = _.filter(this.store.Lanes, l => l.state === 'orange')
    let changeList = []
    orangeList.map(o => {
      if (Date.now() - o.lastLightChange >= config.orangeTime) {
        let lightIndex = _.findIndex(this.store.Lanes, {id: o.id})
        if (lightIndex !== -1) {
          this.store.Lanes[lightIndex].state = 'red'
          this.store.Lanes[lightIndex].lastLightChange = Date.now()
          changeList.push(new LightData(o.id, 'red'))
        }
      }
    })
    return changeList
  }

  handleCertainReds () {
    let redList = this.getCertainRed()
    let changeList = []
    redList.map(o => {
      if (UniHelper.laneIdToString(o.id) !== '1.13.0') {
        if (Date.now() - o.lastLightChange >= config.orangeTime) {
          let lightIndex = _.findIndex(this.store.Lanes, {id: o.id})
          if (lightIndex !== -1) {
            this.store.Lanes[lightIndex].state = 'orange'
            this.store.Lanes[lightIndex].lastLightChange = Date.now()
            changeList.push(new LightData(o.id, 'orange'))
          }
        }
      }
    })
    return changeList
  }

  generateRedPriorityList () {
    let redLights = _.filter(this.store.Lanes, l => l.state === 'red' && (l.primaryTrigger || l.secondaryTrigger))
    let scoredRedLights = redLights.map(light => {
      let score = this.calculateRedScore(light)
      light.score = score
      return light
    })
    return scoredRedLights
  }

  getActiveList () {
    return _.filter(this.store.Lanes, l => l.state === 'green' || l.state === 'orange')
  }

  getCertainRed () {
    let certainRed = _.filter(this.store.Lanes, l => {
      return l.state === 'green' && l.primaryTrigger === false && l.secondaryTrigger === false && (Date.now() - l.lastTriggerChange > config.triggerDeactivate)
    })
    return certainRed
  }

  calculateRedScore (lane) {
    if (lane instanceof Lane) {
      if (lane.primaryTrigger || lane.secondaryTrigger) {
        let score = 0
        if (lane.id.typeId === 1) {
          score += lane.primaryTrigger ? 1 : 0
          score += lane.secondaryTrigger ? 1 : 0
          score += lane.primaryTrigger && lane.secondaryTrigger ? 1 : 0
          let redTime = Date.now() - lane.lastLightChange
          let redTimePercentage = (redTime / (config.maxRedTime / 100)) / 100
          score += 2 * redTimePercentage
          score += lane.weight
        } else if (lane.id.typeId === 2 || lane.id.typeId === 3) {
          if (Date.now() - lane.lastLightChange >= config.minBikePedestrianTime) {
            // console.log('high score')
            score += 100
          }
        } else if (lane.id.typeId === 4) {
          if (Date.now() - lane.lastLightChange >= config.minBridgeIntervalTime) {
            score += 100
          }
        }
        return score
      }
    }
  }

  writeEverythingRed (except13) {
    let lightDataArray = this.store.Lanes.map(ld => {
      let lightData = new LightData(ld.id, 'red')
      if (UniHelper.laneIdToIdiotString(ld.id) === '1.13') {
        lightData.status = except13 ? 'green' : 'red'
      }
      return lightData
    })
    // console.log(lightDataArray)
    let message = dataOut.getTrafficLightsResponse(lightDataArray)
    this.socket.write(message + '\n')
    this.updateWindow()
  }

  writeStoredState () {
    let lightDataArray = this.store.Lanes.map(ld => {
      return new LightData(UniHelper.stringToLaneId(ld.id), ld.state)
    })
    // console.log(lightDataArray)
    let message = dataOut.getTrafficLightsResponse(lightDataArray)
    this.socket.write(message + '\n')
    this.updateWindow()
  }
}

export default CarRouter
