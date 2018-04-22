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
          if (_.indexOf(Ids, UniHelper.laneIdToString(ls.id)) !== -1) {
            if (ls.status === 'green' || ls.status === 'red' || ls.status === 'orange') {
              let idiotId = UniHelper.laneIdToIdiotString(ls.id)
              response.trafficLights.push({
                id: idiotId,
                status: ls.status
              })
            }
          }
        }
      })

      return JSON.stringify(response)
    }
  }
}

export default OutgoingDataFactory
