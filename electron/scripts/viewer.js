const _ = require('lodash')

var store
var manualState = false

document.addEventListener('DOMContentLoaded', () => {
  console.log('DOM was loaded')

  // Add electron communication
  var ipcRenderer = require('electron').ipcRenderer
  // Send electron ready signal
  // ipcRenderer.send('did-finish-load')

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
    var id = divObject.id
    divObject.addEventListener('click', () => {
      if (manualState) {
        ipcRenderer.send('changeState', id)
      } else {
        alert('you have to enable manual mode')
      }
    }, false)
    // console.log(id)
  })
}, false)

// update store div colors
function updateStore () {
  _.forEach(store.lanes, lane => {
    var idThiny = document.getElementById(laneIdToString(lane.id))
    idThiny.style.backgroundColor = lane.state
  })
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

// convert laneId object to string
function laneIdToString (id) {
  if (id.typeId === 3) {
    return id.typeId + '.' + id.lightId + '.' + id.identityId
  } else {
    return id.typeId + '.' + id.lightId
  }
}
