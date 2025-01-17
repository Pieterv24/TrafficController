import _ from 'lodash'

import StorageInstance from '../storage/StorageInstance'
import UniHelper from '../helpers/UnidiotifyHelper'
import { LaneId } from '../models'

class IncommingDataHandler {
  constructor (store, updateWindow) {
    if (store instanceof StorageInstance) {
      this.store = store
      this.updateWindow = updateWindow
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
      let laneIndex = _.findIndex(this.store.Lanes, {'id': new LaneId(2, 0, 0)})
      if ((fixedId.typeId === 2 || fixedId.typeId === 3) && laneIndex !== -1) {
        if (primary) {
          if (this.store.Lanes[laneIndex].primaryTrigger !== dataObject.triggered) {
            this.store.Lanes[laneIndex].lastTriggerChange = Date.now()
            this.store.Lanes[laneIndex].primaryTrigger = JSON.parse(dataObject.triggered)
          }
        } else {
          if (this.store.Lanes[laneIndex].secondaryTrigger !== dataObject.triggered) {
            this.store.Lanes[laneIndex].lastTriggerChange = Date.now()
            this.store.Lanes[laneIndex].secondaryTrigger = JSON.parse(dataObject.triggered)
          }
        }
      } else {
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
          // console.log(this.store.Lanes[laneIndex])
          this.updateWindow()
        }
      }
    }
  }

  handleBridgeChange (dataObject) {
    if (dataObject.opened !== undefined) {
      if (this.store.Bridge.open !== dataObject.opened) {
        this.store.Bridge.changing = false
        this.store.Bridge.open = dataObject.opened
        this.store.Bridge.lastChanged = Date.now()
        if (dataObject.opened) {
          this.store.Bridge.lastOpened = Date.now()
        } else {
          this.store.Bridge.lastClosed = Date.now()
        }
        this.updateWindow()
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
