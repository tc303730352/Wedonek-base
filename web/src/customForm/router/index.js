import BasicLayout from '@/layout/basicLayout'
import Layout from '@/layout'

export const routes = [
  {
    path: '/form',
    component: BasicLayout,
    children: [
      {
        path: '/form/control/edit/:id',
        component: () => import('@/customForm/views/control/editControl'),
        name: 'editControl',
        hidden: true,
        meta: { title: '编辑控件资料', affix: false }
      },
      {
        path: '/form/control/add',
        component: () => import('@/customForm/views/control/editControl'),
        name: 'addControl',
        hidden: true,
        meta: { title: '新增控件资料', affix: false }
      }
    ]
  }
]
