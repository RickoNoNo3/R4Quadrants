import {combineReducers, createStore} from 'redux';

const ActionTypes = {
	MODEL_OPEN: 0x00,
	MODEL_CLOSE: 0x01,
	NET_DOWNLOAD_TODOS: 0x10,
	NET_UPLOAD_TODOS: 0x11,
};

const DefaultState = {
	model: null,
	net: {
		status: 0,
		todos: [[], [], [], []],
	},
};

const ModelReducer = function (state = DefaultState.model, action) {
	if ((action.type & 0xF0) !== 0x00)
		return state;
	switch (action.type) {
		case ActionTypes.MODEL_OPEN:
			return action.data;
		case ActionTypes.MODEL_CLOSE:
			return null;
		default:
			return state;
	}
};

const NetReducer = function (state = DefaultState.net, action) {
	if ((action.type & 0xF0) !== 0x10)
		return state;
	return state;
};

const Store = createStore(combineReducers({
	model: ModelReducer,
	net: NetReducer,
}));

export {ActionTypes, Store};