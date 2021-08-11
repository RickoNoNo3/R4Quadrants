import {combineReducers, createStore} from 'redux';
import axios from 'axios';
import ErrorModel from '../Model/Error';

const SERVER_LOC = 'http://localhost:13809/';

// ActionType的子域掩码
const ActionMask = 0xF0;
// ActionType的子域枚举值
const ActionCategory = {
  MODEL: 0x00,
  NET: 0x10,
  TASK: 0x20,
};
// ActionType的枚举值
const ActionTypes = {
  MODEL_OPEN: 0x00,
  MODEL_CLOSE: 0x01,
  NET_UPDATE: 0x10,
  NET_DOWNLOAD_TASKLIST: 0x11,
  NET_UPLOAD_TASKLIST: 0x12,
  TASK_UPDATE: 0x20,
};

const DefaultState = {
  model: null,
  net: {
    status: 0,
  },
  task: {
    list: [[], [], [], []],
  },
};

// 检查该action的type是否属于某一actionCategory
const CheckCategory = function (action, category) {
  return (action.type & ActionMask) === category;
};

const ModelReducer = function (state = DefaultState.model, action) {
  if (!CheckCategory(action, ActionCategory.MODEL))
    return state;
  switch (action.type) {
    case ActionTypes.MODEL_OPEN:
      return action.modelAction;
    case ActionTypes.MODEL_CLOSE:
      return null;
    default:
      return state;
  }
};

const NetReducer = function (state = DefaultState.net, action) {
  if (!CheckCategory(action, ActionCategory.NET))
    return state;
  switch (action.type) {
    case ActionTypes.NET_DOWNLOAD_TASKLIST: {
      axios.get(`${SERVER_LOC}taskList`)
      .then(res => {
        Store.dispatch({
          type: ActionTypes.NET_UPDATE,
          status: 0,
        });
        Store.dispatch({
          type: ActionTypes.TASK_UPDATE,
          taskList: res.data['taskList'],
        });
      })
      .catch(reason => {
        Store.dispatch({
          type: ActionTypes.NET_UPDATE,
          status: 0,
        });
        Store.dispatch({
          type: ActionTypes.MODEL_OPEN,
          modelAction: ErrorModel('网络错误', '无法获取任务列表'),
        });
        console.log(reason);
      });
      return {...state, status: 1};
    }
    case ActionTypes.NET_UPLOAD_TASKLIST: {
      break;
    }
    case ActionTypes.NET_UPDATE: {
      return {...state, status: action.status};
    }
    default:
      return state;
  }
};

const TaskReducer = function (state = DefaultState.task, action) {
  if (!CheckCategory(action, ActionCategory.TASK))
    return state;
  switch (action.type) {
    case ActionTypes.TASK_UPDATE:
      return {...state, list: action.taskList};
    default:
      return state;
  }
};

const Store = createStore(combineReducers({
  model: ModelReducer,
  net: NetReducer,
  task: TaskReducer,
}));

export {ActionTypes, Store};