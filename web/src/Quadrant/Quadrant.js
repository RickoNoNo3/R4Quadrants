import React from 'react';
import PropTypes from 'prop-types';
import {Store} from '../Redux/Redux';
import Todo from './Todo/Todo';

class Quadrant extends React.Component {
  static propTypes = {
    color: PropTypes.string.isRequired,
    index: PropTypes.number.isRequired,
  };

  render() {
    return (
      <div className="Quadrant Shadowed"
           style={{backgroundColor: this.props.color}}>
        <div>
          {Store.getState().task.list[this.props.index].map((value, i) => (<Todo index={i} key={i} todo={value} content={value.content} title={value.title}/>))}
        </div>
      </div>
    );
  }
}

export default Quadrant;
