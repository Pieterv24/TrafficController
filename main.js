import net from 'net'
import readline from 'readline'

import Store from './storage/StorageInstance'
import DataInHandler from './handlers/IncommingDataHandler'
import DataOutHandler from './handlers/OutgoingDataHandler'
import CarRouter from './logic/CarRouter';

const port = 1234
const ipAddress = 'localhost'

let server = net.createServer((socket) => {
  socket.setEncoding('utf8')
  console.log('Connection established with: %s', socket.remoteAddress)
  let rl = readline.createInterface(socket, socket)

  // create store for the session
  const store = new Store()
  store.init()
  const dataInHandler = new DataInHandler(store)
  const dataOutHandler = new DataOutHandler(socket, store)
  const carRouter = new CarRouter(socket, store)

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
