import React from 'react';
import './Todo.css';
import {ActionTypes, Store} from '../../Redux/Redux';
import DeleteTodoModel from '../../Model/DeleteTodo';

class Todo extends React.Component {
	state = {
		title: '默认',
	};

	showDeleteModel = () => {
		Store.dispatch({
			type: ActionTypes.MODEL_OPEN,
			data: DeleteTodoModel(),
		});
	};

	render() {
		return (
			<div className="Todo">
				<div className="Todo-handle">
					<i className="material-icons-outlined">dehaze</i>
				</div>
				<div className="Todo-text">
					{this.state.title}
				</div>
				<div className="Todo-btns">
					<div className="Todo-btn" onClick={this.showDeleteModel}>
						<i className="material-icons-outlined">delete_forever</i>
					</div>
				</div>
			</div>
		);
	}
}

export default Todo;