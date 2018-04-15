import {sprintf} from 'sprintf-js'

import LaneId from '../models/LaneId'

class UnidiotifyHelper {
  static stringToLaneId (stringId) {
    if (typeof stringId === 'string') {
      let matches = /^([0-9]{1,2})\.([0-9]{1,2})(?:\.([0-9]{1,2}))?$/g.exec(stringId)
      if (matches.length > 1) {
        if (matches[3] === undefined) {
          matches[3] = 0
        }
        return new LaneId(Number(matches[1]), Number(matches[2]), Number(matches[3]))
      }

      throw new Error('No matches found')
    }
    throw new Error('Parameter was not a string')
  }

  static laneIdToString (laneId) {
    if (laneId instanceof LaneId) {
      if (laneId.typeId === 3) {
        return sprintf('%(typeId)s.%(lightId)s.%(identityId)s', laneId)
      } else {
        return sprintf('%(typeId)s.%(lightId)s', laneId)
      }
    } else {
      throw new Error('laneId must be an instance of the LaneId model')
    }
  }

  static fixStringInternal (stringId) {
    let laneId = this.stringToLaneId(stringId)
    return sprintf('%(typeId)s.%(lightId)s.%(identityId)s', laneId)
  }

  static fixStringExternal (stringId) {
    let laneId = this.stringToLaneId(stringId)
    return this.laneIdToString(laneId)
  }
}

export default UnidiotifyHelper
