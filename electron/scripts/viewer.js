const _ = require('lodash')

var store
var manualState = false

document.addEventListener('DOMContentLoaded', () => {
  console.log('DOM was loaded')

  // Add electron communication
  var ipcRenderer = require('electron').ipcRenderer

  // Set event handler for store update
  ipcRenderer.on('updateStore', function (ev, data) {
    console.log('updating storeData')
    store = data
    updateStore()
  })

  // Register manualToggle
  var manualToggle = document.getElementById('manual-toggle')
  manualToggle.innerText = 'Manual: OFF'
  manualToggle.style.backgroundColor = 'red'
  manualToggle.addEventListener('click', () => {
    manualState = !manualState
    updateManualToggle()
    ipcRenderer.send('toggleManual', manualState)
  }, false)

  var idDivs = document.getElementsByClassName('id')
  _.forEach(idDivs, (divObject) => {
    // console.log(divObject)
    var id = divObject.parentElement.id
    console.log(divObject)
    divObject.addEventListener('click', () => {
      if (manualState) {
        ipcRenderer.send('changeState', id)
      } else {
        alert('you have to enable manual mode')
      }
    }, false)
  })
}, false)

// update store div colors
function updateStore () {
  console.log(store)

  // Update Lanes
  _.forEach(store.lanes, lane => {
    var container = document.getElementById(laneIdToString(lane.id))
    var idState = container.getElementsByClassName('id')[0]
    var idPrimaryTrigger = container.getElementsByClassName('primaryTrigger')[0]
    var idSecondaryTrigger = container.getElementsByClassName('secondaryTrigger')[0]
    idState.style.backgroundColor = lane.state

    if (idPrimaryTrigger) {
      idPrimaryTrigger.textContent = lane.primaryTrigger ? '1: ON' : '1: OFF'
    }

    if (idSecondaryTrigger) {
      idSecondaryTrigger.textContent = lane.secondaryTrigger ? '2: ON' : '2: OFF'
    }
  })

  // Update Bridge
  console.log(store.bridge)
  document.getElementById('bridgeStatus').textContent = getBridgeState(store.bridge.open, store.bridge.changing)
}

function updateManualToggle () {
  var manualToggle = document.getElementById('manual-toggle')
  if (manualState) {
    manualToggle.innerText = 'Manual: ON'
    manualToggle.style.backgroundColor = 'green'
  } else {
    manualToggle.innerText = 'Manual: OFF'
    manualToggle.style.backgroundColor = 'red'
  }
}

function getBridgeState (open, action) {
  if (open && !action) {
    return 'Open'
  } else if (open && action) {
    return 'Closing'
  } else if (!open && !action) {
    return 'Closed'
  } else if (!open && action) {
    return 'Opening'
  }
}

// convert laneId object to string
function laneIdToString (id) {
  if (id.typeId === 3) {
    return id.typeId + '.' + id.lightId + '.' + id.identityId
  } else {
    return id.typeId + '.' + id.lightId
  }
}
