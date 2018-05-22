import _ from 'lodash'

import UniHelper from '../helpers/UnidiotifyHelper'
import { LightData } from '../models'

import Ids from '../ids.json'

class OutgoingDataFactory {
  static getBridgeResponse (state) {
    return JSON.stringify({
      type: 'BridgeData',
      bridgeOpen: state
    })
  }

  static getTrafficLightResponse (lightStatus) {
    if (lightStatus instanceof LightData) {
      if (_.indexOf(Ids, UniHelper.laneIdToString(lightStatus.id)) !== -1) {
        if (lightStatus.status === 'green' || lightStatus.status === 'red' || lightStatus.status === 'orange') {
          let idiotId = UniHelper.laneIdToIdiotString(lightStatus.id)
          return JSON.stringify({
            type: 'TrafficLightData',
            trafficLights: [
              {
                id: idiotId,
                lightStatus: lightStatus.status
              }
            ]
          })
        }
      }
    }
  }

  static getTrafficLightsResponse (lightstatuses) {
    if (lightstatuses instanceof Array) {
      let response = {
        type: 'TrafficLightData',
        trafficLights: []
      }

      lightstatuses.map(ls => {
        if (ls instanceof LightData) {
          if (ls.id.typeId === 2) {
            this.getBikeandPedestrianIds().map((id) => {
              let idiotId = UniHelper.fixStringExternal(id)
              if (ls.status === 'green' || ls.status === 'red' || ls.status === 'orange') {
                response.trafficLights.push({
                  id: idiotId,
                  lightStatus: ls.status
                })
              }
            })
          }
          if (_.indexOf(Ids, UniHelper.laneIdToString(ls.id)) !== -1) {
            if (ls.status === 'green' || ls.status === 'red' || ls.status === 'orange') {
              let idiotId = UniHelper.laneIdToIdiotString(ls.id)
              response.trafficLights.push({
                id: idiotId,
                lightStatus: ls.status
              })
            }
          }
        }
      })

      return JSON.stringify(response)
    }
  }

  static getBikeandPedestrianIds () {
    let ids = _.filter(Ids, (o) => {
      let fixedId = UniHelper.stringToLaneId(o)
      return fixedId.typeId === 3 || fixedId.typeId === 2
    })

    return ids
  }

  static getBikeandPedestrianResponse (lightStatus) {
    let response = {
      type: 'TrafficLightData',
      trafficLights: []
    }
    if (lightStatus.id.typeId === 2) {
      if (lightStatus instanceof LightData) {
        _.foreach(Ids, (id) => {
          let fixedId = UniHelper.stringToLaneId(id)
          if (fixedId.typeId === 2 && fixedId.typeId === 3) {
            response.trafficLights.push({
              id: id,
              lightStatus: lightStatus.status
            })
          }
        })
      }
    }
  }
}

export default OutgoingDataFactory
