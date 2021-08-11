import React from 'react';
import PropTypes from 'prop-types';
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
		onClick: PropTypes.func,
		className: PropTypes.string,
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