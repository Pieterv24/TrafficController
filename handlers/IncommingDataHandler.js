import _ from 'lodash'

import StorageInstance from '../storage/StorageInstance'
import UniHelper from '../helpers/UnidiotifyHelper'

class IncommingDataHandler {
  constructor (store) {
    if (store instanceof StorageInstance) {
      this.store = store
    } else {
      throw new Error('supplied store must be an instance of StorageInstance')
    }
  }

  handleIncommingMessage (data, socket) {
    try {
      let dataObject = JSON.parse(data)
      if (typeof dataObject.type === 'string') {
        switch (dataObject.type) {
          case 'PrimaryTrigger':
            this.handleTrigger(dataObject, true)
            break
          case 'SecondaryTrigger':
            this.handleTrigger(dataObject, false)
            break
          case 'BridgeStatusData':
            this.handleBridgeChange(dataObject)
            break
          case 'TimeScaleData':
            this.handleTimeScaleChange(dataObject, socket)
            break
        }
      }
    } catch (err) {
      if (err instanceof SyntaxError) {
        console.log('bad JSON received')
        console.log(err)
      } else {
        throw err
      }
    }
  }

  handleTrigger (dataObject, primary) {
    if (dataObject.triggered !== undefined && dataObject.id !== undefined) {
      let fixedId = UniHelper.stringToLaneId(dataObject.id)
      let laneIndex = _.findIndex(this.store.Lanes, {'id': fixedId})
      if (laneIndex !== -1) {
        if (primary) {
          let currentState = this.store.Lanes[laneIndex].primaryTrigger
          if (currentState !== dataObject.triggered) {
            this.store.Lanes[laneIndex].lastTriggerChange = Date.now()
            this.store.Lanes[laneIndex].primaryTrigger = JSON.parse(dataObject.triggered)
          }
        } else {
          let currentState = this.store.Lanes[laneIndex].secondaryTrigger
          if (currentState !== dataObject.triggered) {
            this.store.Lanes[laneIndex].lastTriggerChange = Date.now()
            this.store.Lanes[laneIndex].secondaryTrigger = JSON.parse(dataObject.triggered)
          }
        }
        console.log(this.store.Lanes[laneIndex])
      }
    }
  }

  handleBridgeChange (dataObject) {
    if (dataObject.opened !== undefined) {
      if (this.store.Bridge.open !== dataObject.opened) {
        this.store.Bridge.changing = false
        this.store.Bridge.open = dataObject.opened
        this.store.Bridge.lastChanged = Date.now()
      }
    }
  }

  handleTimeScaleChange (dataObject, socket) {
    let response = {
      type: 'TimeScaleData',
      status: false
    }

    socket.write(JSON.stringify(response))
  }
}

export default IncommingDataHandler
