class Lane {
  constructor (id) {
    this.id = id
    this.state = 'red'
    this.primaryTrigger = false
    this.secondaryTrigger = false
    this.lastLightChange = Date.now()
    this.lastTriggerChange = Date.now()
    this.weight = 1
  }
}

export default Lane
