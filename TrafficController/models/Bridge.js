class Bridge {
  constructor () {
    this.open = false
    this.changing = false
    this.lastChanged = Date.now()
    this.lastOpened = Date.now()
    this.lastClosed = Date.now()
  }
}

export default Bridge
