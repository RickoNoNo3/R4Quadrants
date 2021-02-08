import React from 'react';
import Todo from './Todo/Todo';
import ReactPropTypes from 'proptypes/src';

class Quadrant extends React.Component {
	static propTypes = {
		color: ReactPropTypes.string.isRequired,
	};

	state = {
		todos: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21],
	};

	render() {
		return (
			<div className="Quadrant Shadowed"
					 style={{backgroundColor: this.props.color}}>
				<div>
					{this.state.todos.map(value => (<Todo key={value} todoId={value}/>))}
				</div>
			</div>
		);
	}
}

export default Quadrant;
