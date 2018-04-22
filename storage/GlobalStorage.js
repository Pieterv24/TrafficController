import Lane from '../models/Lane'
import Bridge from '../models/Bridge'
import UniHelper from '../helpers/UnidiotifyHelper'

// import initialState from '../initialState.json'
import ids from '../ids.json'

let Data = {}
let initRan = false

class GlobalStorage {
  static init () {
    Data = {
      bridge: new Bridge(),
      lanes: []
    }
    ids.map(id => {
      Data.lanes.push(new Lane(UniHelper.stringToLaneId(id)))
    })
    initRan = true
  }

  static get Lanes () {
    if (!initRan) {
      this.init()
    }
    return Data.lanes
  }

  static get Bridge () {
    if (!initRan) {
      this.init()
    }
    return Data.bridge
  }
}

export default GlobalStorage
