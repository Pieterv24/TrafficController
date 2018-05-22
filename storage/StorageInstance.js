import UniHelper from '../helpers/UnidiotifyHelper'
import Lane from '../models/Lane'
import Bridge from '../models/Bridge'

import ids from '../ids.json'
import LaneId from '../models/LaneId';

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
      let laneId = UniHelper.stringToLaneId(id)
      if (laneId.typeId !== 2 && laneId.typeId !== 3) {
        this.data.lanes.push(new Lane(laneId))
      }
    })
    this.data.lanes.push(new Lane(new LaneId(2, 0, 0)))
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
