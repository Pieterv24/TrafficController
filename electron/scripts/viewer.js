var store

document.addEventListener('DOMContentLoaded', () => {
  console.log('DOM was loaded')
  var ipcRenderer = require('electron').ipcRenderer
  ipcRenderer.send('did-finish-load')
  ipcRenderer.on('updateStore', function (ev, data) {
    console.log('updating storeData')
    store = data
    updateStore()
  })
}, false)

function updateStore () {
  store.Lanes.map(l => {
    
  })
}
