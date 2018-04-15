import _ from 'lodash'

import UniHelper from '../helpers/UnidiotifyHelper'

import Ids from '../ids.json'

class OutgoingDataFactory {
  static getBridgeResponse (state) {
    return JSON.stringify({
      type: 'BridgeData',
      bridgeOpen: state
    })
  }

  static getTrafficLightResponse (id, status) {
    if (_.indexOf(Ids, UniHelper.fixStringInternal(id)) !== -1) {
      if (status === 'green' || status === 'red' || status === 'orange') {
        let idiotId = UniHelper.fixStringExternal(id)
        return JSON.stringify({
          type: 'TrafficLightData',
          trafficLights: [
            {
              id: idiotId,
              lightStatus: status
            }
          ]
        })
      }
    }
  }

  static getTrafficLightsResponse (lightstatuses) {
    let response = {
      type: 'TrafficLightData',
      trafficLights: []
    }

    lightstatuses.map(ls => {
      if (_.indexOf(Ids, UniHelper.fixStringInternal(ls.id)) !== -1) {
        if (ls.status === 'green' || ls.status === 'red' || ls.status === 'orange') {
          let idiotId = UniHelper.fixStringExternal(ls.id)
          response.trafficLights.push({
            id: idiotId,
            status: ls.status
          })
        }
      }
    })

    return JSON.stringify(response)
  }
}

export default OutgoingDataFactory
