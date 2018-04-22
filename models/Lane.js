import LaneId from './LaneId'

class Lane {
  constructor (id) {
    if (id instanceof LaneId) {
      this.id = id
      this.state = 'red'
      this.primaryTrigger = false
      this.secondaryTrigger = false
      this.lastLightChange = Date.now()
      this.lastTriggerChange = Date.now()
      this.weight = 1
    } else {
      throw Error
    }
  }
}

export default Lane
