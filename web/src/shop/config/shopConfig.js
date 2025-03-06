module.exports = {
  EnumDic: {
    relationType: 'AdvertRelationType',
    userRange: 'ActivityUserRange',
    activityType: 'ActivityType',
    range: 'DiscountRangeType',
    threshold: 'DiscountThreshold',
    disContent: 'DiscountContent',
    couponType: 'CouponType',
    couponStatus: 'CouponStatus',
    receiveType: 'CouponLimitReceiveType',
    pageSkuOrderType: 'PageSkuOrderType',
    pageUseRange: 'PageUseRange',
    pageStatus: 'PageStatus'
  },
  DictItemDic: {
    placeType: 5
  },
  AreaType: {
    0: {
      text: '未知'
    },
    1: {
      text: '国家'
    },
    2: {
      text: '省份'
    },
    3: {
      text: '城市'
    },
    4: {
      text: '区县'
    }
  },
  ValuationMode: {
    0: {
      text: '计重',
      value: 0
    },
    1: {
      text: '计重和体积',
      value: 1
    },
    2: {
      text: '计体积',
      value: 2
    },
    3: {
      text: '固定金额',
      value: 3
    }
  },
  RelationType: {
    0: {
      text: '商品',
      value: 0
    },
    1: {
      text: '活动',
      value: 1
    }
  },
  PlaceStatus: {
    0: {
      text: '起草',
      value: 0,
      color: '#82848a'
    },
    1: {
      text: '已启用',
      value: 1,
      color: '#43AF2B'
    },
    2: {
      text: '已停用',
      value: 2,
      color: '#e4393c'
    }
  },
  AdvertStatus: {
    0: {
      text: '起草',
      value: 0,
      color: '#82848a'
    },
    1: {
      text: '已启用',
      value: 1,
      color: '#43AF2B'
    },
    2: {
      text: '已停用',
      value: 2,
      color: '#e4393c'
    },
    3: {
      text: '投放中',
      value: 3,
      color: '#1890ff'
    },
    4: {
      text: '已结束',
      value: 3,
      color: '#F3A70F'
    }
  },
  GoodsStatus: {
    0: {
      text: '起草',
      value: 0,
      color: '#1890ff'
    },
    1: {
      text: '发布中',
      value: 1,
      color: '#F3A70F'
    },
    2: {
      text: '已发布',
      value: 2,
      color: '#43AF2B'
    },
    3: {
      text: '下架',
      value: 3,
      color: '#e4393c'
    }
  },
  UserRegMode: {
    'WxMini': {
      text: '微信小程序',
      value: 'WxMini',
      color: '#1890ff'
    }
  },
  UserStatus: {
    0: {
      text: '禁用',
      value: 0,
      color: '#F3A70F'
    },
    1: {
      text: '启用',
      value: 1,
      color: '#43AF2B'
    },
    2: {
      text: '已删除',
      value: 2,
      color: '#e4393c'
    }
  },
  ActivityStatus: {
    0: {
      text: '起草',
      value: 0,
      color: '#82848a'
    },
    1: {
      text: '待审核',
      value: 1,
      color: '#1890ff'
    },
    2: {
      text: '已审核',
      value: 2,
      color: '#43AF2B'
    },
    3: {
      text: '已下架',
      value: 3,
      color: '#e4393c'
    },
    4: {
      text: '审核未通过',
      value: 3,
      color: '#F3A70F'
    }
  },
  CouponStatus: {
    0: {
      text: '起草',
      value: 0,
      color: '#82848a'
    },
    1: {
      text: '启用',
      value: 1,
      color: '#43AF2B'
    },
    2: {
      text: '停用',
      value: 2,
      color: '#F3A70F'
    },
    3: {
      text: '已下架',
      value: 3,
      color: '#e4393c'
    },
    4: {
      text: '投放结束',
      value: 3,
      color: '#1890ff'
    }
  },
  ActivityType: {
    0: {
      text: '单品促销',
      value: 0
    },
    1: {
      text: '店铺促销',
      value: 1
    },
    2: {
      text: '套装促销',
      value: 2
    },
    3: {
      text: '单品秒杀',
      value: 3
    }
  },
  CouponType: {
    0: {
      text: '折扣卷',
      value: 0
    },
    1: {
      text: '满减',
      value: 1
    }
  },
  PageUseRange: {
    1: {
      text: '小程序',
      value: 1
    },
    0: {
      text: 'PC',
      value: 0
    }
  },
  PageStatus: {
    0: {
      text: '起草',
      value: 0,
      color: '#82848a'
    },
    1: {
      text: '已发布',
      value: 1,
      color: '#43AF2B'
    },
    2: {
      text: '已停用',
      value: 2,
      color: '#e4393c'
    }
  }
}
