import BasicLayout from '@/layout/basicLayout'

export const routes = [
  {
    path: '/form',
    component: BasicLayout,
    children: [
      {
        path: '/form/body/edit/:id',
        component: () => import('@/customForm/views/form/formEdit'),
        name: 'formBodyEdit',
        hidden: true,
        meta: { title: '编辑表单结构', affix: false }
      }
    ]
  }
]
