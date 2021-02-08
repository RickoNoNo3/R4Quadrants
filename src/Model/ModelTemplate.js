import React from 'react';
import ReactPropTypes from 'proptypes/src';
import {ActionTypes, Store} from '../Redux/Redux';

class ModelBtnBar extends React.Component {
	render() {
		return (
			<div className="ModelBtnBar">
				{this.props.children}
			</div>
		);
	}
}

class ModelBtn extends React.Component {
	static propTypes = {
		onClick: ReactPropTypes.func,
		className: ReactPropTypes.string,
	};

	handleClick = () => {
		this.props.onClick && this.props.onClick();
		Store.dispatch({
			type: ActionTypes.MODEL_CLOSE,
		});
	};

	render() {
		return (
			<div className={`ModelBtn ${this.props.className}`}
					 onClick={this.handleClick}>
				{this.props.children}
			</div>
		);
	}
}

export {ModelBtnBar, ModelBtn};