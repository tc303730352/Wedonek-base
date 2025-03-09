module.exports = {
  HrEnumDic: {
    deptStatus: 'HrDeptStatus',
    hrUserType: 'HrUserType',
    hrWorkStatus: 'HrWorkStatus',
    hrUserSex: 'HrUserSex',
    dicStatus: 'DicStatus',
    dicItemStatus: 'DicItemStatus',
    hrPowerType: 'PowerType'
  },
  HrItemDic: {
    deptTag: '3',
    post: '2',
    title: '1',
    nation: '4'
  },
  DicItemStatus: {
    1: {
      text: '启用',
      color: '#43AF2B',
      status: 'success'
    },
    2: {
      text: '停用',
      color: '#999',
      status: 'danger'
    },
    0: {
      text: '起草',
      color: '#1890ff',
      status: 'info'
    }
  },
  HrUserType: {
    1: {
      text: '实习'
    },
    2: {
      text: '正式工'
    },
    3: {
      text: '临时工'
    },
    0: {
      text: '未设置'
    }
  },
  HrDeptStatus: {
    1: {
      text: '启用',
      color: '#43AF2B',
      status: 'success'
    },
    2: {
      text: '停用',
      color: '#999',
      status: 'danger'
    },
    0: {
      text: '起草',
      color: '#1890ff',
      status: 'info'
    }
  },
  HrEmpStatus: {
    1: {
      text: '启用',
      color: '#43AF2B',
      value: 1,
      status: 'success'
    },
    2: {
      text: '停用',
      color: '#999',
      value: 2,
      status: 'danger'
    },
    0: {
      text: '起草',
      color: '#1890ff',
      value: 0,
      status: 'info'
    }
  }
}
