import _ from 'lodash'

import Lane from '../models/Lane'

import matrix from '../conflictMatrix.json'
import config from '../config.json'

class CarRouter {
  constructor (socket, store) {
    this.socket = socket
    this.store = store
    this.matrix = matrix
  }

  generateRedPriorityList () {
    let redLights = _.filter(this.store.Lanes, l => l.state === 'red' && (l.primaryTrigger || l.secondaryTrigger))
    let scoredRedLights = redLights.map(light => {
      let score = this.calculateRedScore(light)
      light.score = score
      return light
    })
    return scoredRedLights
  }

  getCertainRed () {
    let certainRed = _.filter(this.store.Lanes, l => l.state === 'green' && l.primaryTrigger === false && l.secondaryTrigger === false && (Date.now() - l.lastTriggerChange > 10000))
    return certainRed
  }

  calculateRedScore (lane) {
    if (lane instanceof Lane) {
      if (lane.primaryTrigger || lane.secondaryTrigger) {
        let score = 0
        score += lane.primaryTrigger ? 1 : 0
        score += lane.secondaryTrigger ? 1 : 0
        score += lane.primaryTrigger && lane.secondaryTrigger ? 1 : 0
        let redTime = Date.now() - lane.lastLightChange
        let redTimePercentage = (redTime / (config.maxRedTime / 100)) / 100
        score += 2 * redTimePercentage
        score += lane.weight
        return score
      }
    }
  }
}

export default CarRouter
