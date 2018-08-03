import Vue from 'vue'
import Router from 'vue-router'
import MoneyFlowVisualization from '../components/moneyflowvisualization'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'MoneyFlowVisualization',
      component: MoneyFlowVisualization
    }
  ]
})
