import React from 'react';
import {ModelBtn, ModelBtnBar} from './ModelTemplate';

class DeleteTodoContent extends React.Component {
	doDelete = () => {

	};

	render() {
		return (
			<div>
				<p>确定要删除此任务吗?</p>
				<ModelBtnBar>
					<ModelBtn className="Main" onClick={this.doDelete}>确定</ModelBtn>
					<ModelBtn>取消</ModelBtn>
				</ModelBtnBar>
			</div>
		);
	}
}

function DeleteTodoModel(todoId) {
	return {
		title: '删除任务',
		content: (
			<DeleteTodoContent todoId={todoId}/>
		),
	};
}

export default DeleteTodoModel;