import Lane from '../models/Lane'
import Bridge from '../models/Bridge'

import ids from '../ids.json'

class StorageInstance {
  constructor () {
    this.initRan = false
    this.data = {
      bridge: {},
      lanes: []
    }
  }

  init () {
    this.data = {
      bridge: new Bridge(),
      lanes: []
    }
    ids.map(id => {
      this.data.lanes.push(new Lane(id))
    })
    this.initRan = true
  }

  get Lanes () {
    if (!this.initRan) {
      this.init()
    }
    return this.data.lanes
  }

  get Bridge () {
    if (!this.initRan) {
      this.init()
    }
    return this.data.bridge
  }
}

export default StorageInstance
