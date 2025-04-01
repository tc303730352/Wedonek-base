module.exports = {
  EnumDic: {
    ControlType: 'ControlType',
    ControlStatus: 'ControlStatus',
    ControlValueRange: 'ControlValueRange',
    FormStatus: 'FormStatus',
    FormTableType: 'FormTableType',
    ValidateRuleType: 'ValidateRuleType'
  },
  DictItemDic: {
    editControl: 8,
    showControl: 9
  },
  ControlStatus: {
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
  FormStatus: {
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
  }
}
