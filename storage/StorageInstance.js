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
      if (laneId.typeId === 1) {
        let lane = new Lane(laneId)
        if (laneId.lightId === 5) {
          lane.weight = 2
        }
        lane.state = laneId.lightId === 13 ? 'green' : 'red'
        this.data.lanes.push(new Lane(laneId))
      } else if (laneId.typeId === 4) {
        let lane = new Lane(laneId)
        lane.weight = 0.1
        this.data.lanes.push(lane)
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
