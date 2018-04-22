import _ from 'lodash'

import UniHelper from '../helpers/UnidiotifyHelper'
import dataOut from '../logic/OutgoingDataFactory'
import { LightData } from '../models'

class OutgoingDataHandler {
  constructor (store, socket, updateWindow) {
    this.store = store
    this.socket = socket
    this.updateWindow = updateWindow
  }

  toggleLight (id) {
    let unidiotId = UniHelper.stringToLaneId(id)
    let lightIndex = _.findIndex(this.store.Lanes, {id: unidiotId})
    if (lightIndex !== -1) {
      let state = this.nextColor(this.store.Lanes[lightIndex].state)
      this.store.Lanes[lightIndex].state = state
      this.store.Lanes[lightIndex].lastLightChange = Date.now()
      let command = dataOut.getTrafficLightResponse(new LightData(unidiotId, state))
      this.socket.write(command + '\n')
      this.updateWindow()
    }
  }

  nextColor (color) {
    switch (color) {
      case 'red':
        return 'green'
      case 'green':
        return 'orange'
      case 'orange':
        return 'red'
      default:
        return 'red'
    }
  }
}

export default OutgoingDataHandler
