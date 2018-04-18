import _ from 'lodash'

import Lane from '../models/Lane'
import dataOut from './OutgoingDataFactory'

import matrix from '../conflictMatrix.json'
import config from '../config.json'

class CarRouter {
  constructor (socket, store) {
    this.socket = socket
    this.store = store
    this.matrix = matrix

    this.store.Lanes[0].primaryTrigger = true
  }

  doCycle () {
    let changeLightArray = []
    let prioritizedRedList = this.generateRedPriorityList()
    changeLightArray = _.concat(changeLightArray, this.handleOranges(), this.handleCertainReds())
    prioritizedRedList.map(light => {
      let conflicts = _.find(this.matrix, {id: light.id})
      let greenLights = this.getActiveList()
      let conflits = _.filter(greenLights, gl => conflicts.blockedBy.includes(gl.id))
      if (conflits.length > 0) {
        conflicts.map(c => {
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
          changeLightArray.push({id: light.id, status: 'green'})
        }
      }
    })
    let command = dataOut.getTrafficLightsResponse(changeLightArray)
    this.socket.write(command + '\n')
  }

  handleConflict (id) {
    let conflict = _.find(this.store.Lanes, {id: id})
    if (conflict !== undefined) {
      if (conflict.state === 'green' && Date.now() - conflict.lastLightChange > config.minGreenTime) {
        let lightIndex = _.findIndex(this.store.Lanes, {id: conflict.id})
        if (lightIndex !== -1) {
          this.store.Lanes[lightIndex].state = 'green'
          this.store.Lanes[lightIndex].lastLightChange = Date.now()
          return {id: conflict.id, status: 'orange'}
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
          changeList.push({id: o.id, status: 'red'})
        }
      }
    })
    return changeList
  }

  handleCertainReds () {
    let redList = this.getCertainRed()
    let changeList = []
    redList.map(o => {
      if (Date.now() - o.lastLightChange >= config.orangeTime) {
        let lightIndex = _.findIndex(this.store.Lanes, {id: o.id})
        if (lightIndex !== -1) {
          this.store.Lanes[lightIndex].state = 'orange'
          this.store.Lanes[lightIndex].lastLightChange = Date.now()
          changeList.push({id: o.id, status: 'orange'})
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
    return _.filter(this.store.lanes, l => l.state === 'green' || l.state === 'orange')
  }

  getCertainRed () {
    let certainRed = _.filter(this.store.Lanes, l => l.state === 'green' && l.primaryTrigger === false && l.secondaryTrigger === false && (Date.now() - l.lastTriggerChange > 10000))
    return certainRed
  }

  calculateRedScore (lane) {
    if (lane instanceof Lane) {
      if (lane.primaryTrigger || lane.secondaryTrigger) {
        let score = 0
        score += lane.primaryTrigger ? 1 : 0
        score += lane.secondaryTrigger ? 1 : 0
        score += lane.primaryTrigger && lane.secondaryTrigger ? 1 : 0
        let redTime = Date.now() - lane.lastLightChange
        let redTimePercentage = (redTime / (config.maxRedTime / 100)) / 100
        score += 2 * redTimePercentage
        score += lane.weight
        return score
      }
    }
  }
}

export default CarRouter
