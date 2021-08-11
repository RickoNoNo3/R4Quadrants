import React from 'react';
import {ModelBtn, ModelBtnBar} from './ModelTemplate';

class AddTodoContent extends React.Component {
	doAdd = () => {

	};

	render() {
		return (
			<div>
				<form>
					<label>
						<span>任务名称&emsp;</span>
						<input type="text"/>
					</label>
					<label>
						<span>任务类型&emsp;</span>
						<div className="CategorySelector">
							<label className="NoMargin">
								<input type="radio" name="category" value="0"/>
								<span><span style={{backgroundColor: '#774444'}}/>紧急不重要</span>
							</label>
							<label className="NoMargin">
								<input type="radio" name="category" value="1"/>
								<span><span style={{backgroundColor: '#AAAA44'}}/>紧急重要</span>
							</label>
							<label className="NoMargin">
								<input type="radio" name="category" value="2"/>
								<span><span style={{backgroundColor: '#444444'}}/>不紧急不重要</span>
							</label>
							<label className="NoMargin">
								<input type="radio" name="category" value="3"/>
								<span><span style={{backgroundColor: '#443377'}}/>重要不紧急</span>
							</label>
						</div>
					</label>
					<label>
						<span>任务描述&emsp;</span>
						<textarea/>
					</label>
					<ModelBtnBar>
						<ModelBtn className="Main" onClick={this.doAdd}>确定</ModelBtn>
						<ModelBtn>取消</ModelBtn>
					</ModelBtnBar>
				</form>
			</div>
		);
	}
}

function AddTodoModel() {
	return {
		title: '添加任务',
		content: (
			<AddTodoContent/>
		),
	};
}

export default AddTodoModel;
