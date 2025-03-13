module.exports = {
  fileType: {
    'image/jpeg': '.jpg',
    'image/gif': '.gif',
    'image/png': '.png',
    'text/xml': '.xml',
    'text/html': '.html',
    'text/plain': '.txt',
    'application/pdf': '.pdf'
  },
  FileEnumDic: {
    fileType: 'FileType',
    dirStatus: 'FileDirStatus',
    fileSaveType: 'FileSaveType',
    filePower: 'FilePower'
  },
  DirStatus: {
    0: {
      text: '启用',
      value: 0
    },
    1: {
      text: '只读',
      value: 1
    },
    2: {
      text: '禁用',
      value: 2
    }
  },
  FilePower: {
    0: {
      text: '公共',
      value: 0
    },
    1: {
      text: '私有',
      value: 1
    },
    2: {
      text: '指定权限',
      value: 2
    },
    3: {
      text: '共享',
      value: 3
    }
  },
  FileType: {
    0: {
      text: '图片',
      value: 0
    },
    1: {
      text: '视频',
      value: 1
    },
    2: {
      text: '音频',
      value: 2
    },
    3: {
      text: '文件',
      value: 3
    },
    4: {
      text: '文档',
      value: 4
    }
  },
  FileSaveType: {
    0: {
      text: '本地',
      value: 0
    },
    1: {
      text: 'OOS',
      value: 1
    }
  }
}
