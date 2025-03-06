import BasicLayout from '@/layout/basicLayout'
import Layout from '@/layout'

export const routes = [
  {
    path: '/shop',
    component: BasicLayout,
    redirect: '/shop/index',
    children: [
      {
        path: '/shop/index',
        component: () => import('@/shop/views/index'),
        name: 'shopIndex',
        hidden: true,
        meta: { title: '商城系统登陆页', affix: false }
      }
    ]
  },
  {
    path: '/shop/logistics',
    component: Layout,
    children: [
      {
        path: '/shop/logistics/item/:id',
        component: () => import('@/shop/views/logistics/logisticsConfigList'),
        name: 'logisticsItem',
        hidden: true,
        meta: { title: '物流费用明细', affix: false }
      }
    ]
  },
  {
    path: '/shop/page',
    component: Layout,
    children: [
      {
        path: '/shop/page/:id/:range',
        component: () => import('@/shop/views/pageTemplate/editPage'),
        name: 'editPage',
        hidden: true,
        meta: { title: '页面装修', affix: false }
      }
    ]
  },
  {
    path: '/shop/goods',
    component: Layout,
    children: [
      {
        path: '/shop/goods/:id/:cid/edit',
        component: () => import('@/shop/views/goods/editGoods'),
        name: 'goodsEdit',
        hidden: true,
        meta: { title: '编辑商品', affix: false }
      },
      {
        path: '/shop/goods/add',
        component: () => import('@/shop/views/goods/editGoods'),
        name: 'goodsAdd',
        hidden: true,
        meta: { title: '新增商品', affix: false }
      },
      {
        path: '/shop/goods/detailed/:id',
        component: () => import('@/shop/views/goods/goodsDetailed'),
        name: 'goodsDetailed',
        hidden: true,
        meta: { title: '商品详细', affix: false }
      }
    ]
  },
  {
    path: '/shop/activity',
    component: Layout,
    children: [
      {
        path: '/shop/activity/:type/:id/edit',
        component: () => import('@/shop/views/activity/editActivity'),
        name: 'activityEdit',
        hidden: true,
        meta: { title: '编辑活动', affix: false }
      },
      {
        path: '/shop/activity/:type/add',
        component: () => import('@/shop/views/activity/editActivity'),
        name: 'activityAdd',
        hidden: true,
        meta: { title: '新增活动', affix: false }
      },
      {
        path: '/shop/activity/:id/info',
        component: () => import('@/shop/views/activity/activityView'),
        name: 'activityView',
        hidden: true,
        meta: { title: '活动详细', affix: false }
      },
      {
        path: '/shop/activity/:id/audit',
        component: () => import('@/shop/views/activity/auditActivity'),
        name: 'auditActivity',
        hidden: true,
        meta: { title: '审核活动信息', affix: false }
      }
    ]
  },
  {
    path: '/shop/coupon',
    component: Layout,
    children: [
      {
        path: '/shop/coupon/:range/:id/edit',
        component: () => import('@/shop/views/coupon/editCoupon'),
        name: 'couponEdit',
        hidden: true,
        meta: { title: '编辑优惠卷', affix: false }
      },
      {
        path: '/shop/coupon/:range/add',
        component: () => import('@/shop/views/coupon/editCoupon'),
        name: 'couponAdd',
        hidden: true,
        meta: { title: '新增优惠卷', affix: false }
      },
      {
        path: '/shop/coupon/:id/info',
        component: () => import('@/shop/views/coupon/couponView'),
        name: 'couponView',
        hidden: true,
        meta: { title: '优惠卷详细', affix: false }
      }
    ]
  }
]