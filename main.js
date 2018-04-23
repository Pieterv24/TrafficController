import net from 'net'
import readline from 'readline'
// Electron
// const {app, BrowserWindow} = require('electron')
import {app} from 'electron'

import Store from './storage/StorageInstance'
import DataInHandler from './handlers/IncommingDataHandler'
import CarRouter from './logic/CarRouter'
import ElectronWindow from './ElectronWindow'
import DataOutHandler from './handlers/OutgoingDataHandler'

const port = 1234
const ipAddress = 'localhost'
const electronDebug = true

if (process.mas) app.setName('Traffic Controller')

function startController () {
  let server = net.createServer((socket) => {
    socket.setEncoding('utf8')
    console.log('Connection established with: %s', socket.remoteAddress)
    let rl = readline.createInterface(socket, socket)

    // create store for the session
    const store = new Store()
    store.init()
    const sessionWindow = new ElectronWindow(app, store, electronDebug)
    sessionWindow.startWindow()
    const dataInHandler = new DataInHandler(store, () => { sessionWindow.updateStore() })
    const carRouter = new CarRouter(socket, store, () => { sessionWindow.updateStore() })
    const dataOutHandler = new DataOutHandler(store, socket, () => { sessionWindow.updateStore() })

    sessionWindow.dataOutHandler = dataOutHandler
    sessionWindow.carRouter = carRouter

    carRouter.writeEverythingRed()

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
