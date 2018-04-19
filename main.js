import net from 'net'
import readline from 'readline'
import path from 'path'
import glob from 'glob'

// Electron
const {app, BrowserWindow} = require('electron')

import Store from './storage/StorageInstance'
import DataInHandler from './handlers/IncommingDataHandler'
import DataOutHandler from './handlers/OutgoingDataHandler'
import CarRouter from './logic/CarRouter'
import ElectronWindow from './ElectronWindow'

const port = 1234
const ipAddress = 'localhost'

if (process.mas) app.setName('Traffic Controller')

let mainWindow = null

function initialize () {
  const shouldQuit = makeSingleInstance()
  if (shouldQuit) return app.quit()
  function creatWindow () {
    const windowOptions = {
      width: 1080,
      minWidth: 680,
      height: 840,
      title: app.getName()
    }

    mainWindow = new BrowserWindow(windowOptions)
    mainWindow.loadURL(path.join('file://', __dirname, '/index.html'))

    mainWindow.on('closed', () => {
      mainWindow = null
    })
  }

  app.on('ready', () => {
    creatWindow()
  })

  app.on('window-all-closed', () => {
    if (process.platform != 'darwin') {
      app.quit()
    }
  })

  app.on('activate', () => {
    if (mainWindow === null) {
      creatWindow()
    }
  })
}

function makeSingleInstance () {
  if (process.mas) return false
  return app.makeSingleInstance(() => {
    if (mainWindow) {
      if (mainWindow.isMinimized()) mainWindow.restore()
      mainWindow.focus()
    }
  })
}

function startController () {
  let server = net.createServer((socket) => {
    socket.setEncoding('utf8')
    console.log('Connection established with: %s', socket.remoteAddress)
    let rl = readline.createInterface(socket, socket)

    // create store for the session
    const store = new Store()
    store.init()
    const sessionWindow = new ElectronWindow(app, store)
    sessionWindow.startWindow()
    const dataInHandler = new DataInHandler(store)
    const dataOutHandler = new DataOutHandler(socket, store)
    const carRouter = new CarRouter(socket, store, () => {sessionWindow.updateStore()})

    dataOutHandler.everythingRed()
    carRouter.generateRedPriorityList()

    // is ran when line of data is received
    rl.on('line', (data) => {
      dataInHandler.handleIncommingMessage(data, socket)
    })

    setInterval(() => {
      carRouter.doCycle()
    }, 1000)

    socket.on('close', () => {
      sessionWindow.close()
      socket.end()
      console.log('connection closed')
    })

    socket.on('error', (err) => {
      socket.end()
      // throw err
      console.log('Socket closed with an error')
      console.log(err.message)
    })
  })

  server.listen(port, ipAddress, () => {
    console.log('Server is listening on %s : %s', server.address().address, server.address().port)
  })
}

app.on('ready', () => {
  startController()
})
