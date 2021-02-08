import './App.css';
import React from 'react';
import QuadrantGroup from './Quadrant/QuadrantGroup';
import Model from './Model/Model';
import {ActionTypes, Store} from './Redux/Redux';
import AddTodoModel from './Model/AddTodo';

class App extends React.Component {
	componentDidMount() {
		Store.subscribe(() => this.setState({}));
	}

	showAdd = () => {
		Store.dispatch({
			type: ActionTypes.MODEL_OPEN,
			data: AddTodoModel(),
		});
	};

	render() {
		return (
			<div className="App">
				<Model template={Store.getState().model}/>
				<header className="App-header Shadowed">
					<div className="AddBtn" onClick={this.showAdd}>
						<i className="material-icons">add</i>
					</div>
					<p>
						R4Quadrant
					</p>
				</header>
				<QuadrantGroup/>
			</div>
		);
	}
}

export default App;
