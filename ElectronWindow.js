import path from 'path'

import {BrowserWindow, ipcMain} from 'electron'

class ElectronWindow {
  constructor (app, store) {
    this.app = app
    this.window = null
    this.store = store
    this.windowReady = false
  }

  startWindow () {
    const windowOptions = {
      width: 1080,
      minWidth: 680,
      height: 840,
      title: this.app.getName(),
      show: false
    }

    this.window = new BrowserWindow(windowOptions)
    this.window.loadURL(path.join('file://', __dirname, 'electron/index.html'))
    this.window.webContents.openDevTools()
    this.window.on('ready-to-show', () => {
      console.log('window is ready')
      this.window.webContents.send('updateStore', this.store)
      this.window.show()
      this.windowReady = true
    })
    this.window.on('closed', () => {
      this.window = null
    })
  }

  updateStore () {
    if (this.windowReady) {
      this.window.webContents.send('updateStore', this.store)
    }
  }

  close () {
    this.window.close()
  }
}

export default ElectronWindow
