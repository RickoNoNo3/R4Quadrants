import React from 'react';
import './Todo.css';
import {ActionTypes, Store} from '../../Redux/Redux';
import DeleteTodoModel from '../../Model/DeleteTodo';
import PropTypes from 'prop-types';

class Todo extends React.Component {
  static propTypes = {
    index: PropTypes.number.isRequired,
    title: PropTypes.string.isRequired,
    content: PropTypes.string.isRequired,
  };

  state = {
    isInDragging: false,
    pos: [],
  };

  showDeleteModel = () => {
    Store.dispatch({
      type: ActionTypes.MODEL_OPEN,
      modelAction: DeleteTodoModel(),
    });
  };

  /**
   * @param e {DragEvent}
   */
  onDragStart = (e) => {
    const state = {
      isInDragging: true,
      pos: [e.pageY, e.pageX],
    };
    console.log(e);
    this.setState(state);
  };

  /**
   * @param e {DragEvent}
   */
  onDragEnd = (e) => {
    const state = {
      isInDragging: false,
      pos: [],
    };
    this.setState(state);
  };

  /**
   * @param e {MouseEvent}
   */
  onDragOver = (e) => {
    const state = {
      isInDragging: true,
      pos: [e.pageY, e.pageX],
    };
    console.log(e)
    this.setState(state);
  };

  render() {
    return (
      <div className={`Todo ${this.state.isInDragging ? 'Dragging' : ''}`}
           style={{
             top: this.state.pos[0],
             left: this.state.pos[1],
           }}>
        <div className="Todo-handle">
          <i className="material-icons-outlined">dehaze</i>
          <div draggable="true" onDragStart={this.onDragStart} onDragEnd={this.onDragEnd} onDragOver={this.onDragOver}/>
        </div>
        <div className="Todo-text">
          {this.props.title}
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