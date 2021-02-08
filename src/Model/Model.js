import React from 'react';
import './Model.css';
import {ActionTypes, Store} from '../Redux/Redux';
import ReactPropTypes from 'proptypes/src';

class Model extends React.Component {
	static propTypes = {
		template: ReactPropTypes.object,
	};

	closeModel = () => {
		Store.dispatch({
			type: ActionTypes.MODEL_CLOSE,
		});
	};

	render() {
		if (this.props.template == null) {
			return null;
		}
		return (
			<div>
				<div className="Model">
					<div className="Model-bg" onClick={this.closeModel}/>
					<div className="Model-win Shadowed">
						<div className="Model-header">
							<p>{this.props.template.title}</p>
							<div className="Model-header-closeBtn" onClick={this.closeModel}>
								<i className="material-icons">close</i>
							</div>
						</div>
						<div className="Model-content">
							{this.props.template.content}
						</div>
					</div>
				</div>
			</div>
		);
	}
}

export default Model;