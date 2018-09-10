import path from 'path'

import {BrowserWindow, ipcMain} from 'electron'

class ElectronWindow {
  constructor (app, store, debug) {
    this.app = app
    this.window = null
    this.store = store
    this.windowReady = false
    this.debug = false
    this.dataOutHandler = undefined
    this.carRouter = undefined
  }

  startWindow () {
    const windowOptions = {
      width: 1080,
      minWidth: 680,
      height: 840,
      title: 'Traffic Controller',
      show: false
    }

    this.window = new BrowserWindow(windowOptions)
    this.window.setMenu(null)
    this.window.loadURL(path.join('file://', __dirname, 'electron/index.html'))
    this.window.webContents.openDevTools()
    this.window.on('ready-to-show', () => {
      console.log('window is ready')
      this.window.webContents.send('updateStore', this.store.data)
      console.log('Window: ' + this.window.webContents.id + ' was created')

      // Handle incomming stateChange messages
      ipcMain.on('changeState', (sender, id) => {
        if (sender.sender.webContents.id && this.window.webContents.id === sender.sender.webContents.id) {
          console.log('Window: ' + this.window.webContents.id + ' returned: ' + id)
          if (this.dataOutHandler !== undefined) {
            this.dataOutHandler.toggleLight(id)
          }
        }
      })

      // Hanlde manual toggle
      ipcMain.on('toggleManual', (sender, state) => {
        if (sender.sender.webContents.id && this.window.webContents.id === sender.sender.webContents.id) {
          if (this.carRouter !== undefined) {
            console.log('Window: ' + this.window.webContents.id + ' turned manual to: ' + state)
            this.carRouter.manual = state
          }
        }
      })
      this.window.show()
      this.windowReady = true
    })
    this.window.on('closed', () => {
      this.window = null
    })
  }

  updateStore () {
    if (this.windowReady) {
      this.window.webContents.send('updateStore', this.store.data)
    }
  }

  close () {
    this.window.close()
  }
}

export default ElectronWindow
