import OutgoingDataFactory from '../logic/OutgoingDataFactory'
import LightData from '../models/lightData'

class OutgoingDataHandler {
  constructor (socket, store) {
    this.store = store
    this.socket = socket
  }

  everythingRed () {
    let lightDataArray = this.store.Lanes.map(ld => {
      return new LightData(ld.id, ld.state)
    })
    console.log(lightDataArray)
    let message = OutgoingDataFactory.getTrafficLightsResponse(lightDataArray)
    this.socket.write(message + '\n')
  }

  writeStoredState () {
    let lightDataArray = this.store.Lanes.map(ld => {
      return new LightData(ld.id, ld.state)
    })
    console.log(lightDataArray)
    let message = OutgoingDataFactory.getTrafficLightsResponse(lightDataArray)
    this.socket.write(message + '\n')
  }
}

export default OutgoingDataHandler
