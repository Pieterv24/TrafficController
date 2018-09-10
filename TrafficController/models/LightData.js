import LaneId from './LaneId'

class LightData {
  constructor (id, status) {
    if (id instanceof LaneId) {
      this.id = id
      this.status = status
    } else {
      throw Error
    }
  }
}

export default LightData
