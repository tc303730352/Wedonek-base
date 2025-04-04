import Layout from '@/layout'

export const routes = [
  {
    path: '/form',
    component: Layout,
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
