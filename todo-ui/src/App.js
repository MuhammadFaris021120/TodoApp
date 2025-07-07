import React, { useEffect, useState } from 'react';
import axios from 'axios';

const API_URL = 'https://localhost:7077/api/todos'; 

function App() {
  const [todos, setTodos] = useState([]);
  const [newTitle, setNewTitle] = useState('');

  useEffect(() => {
    fetchTodos();
  }, []);

  const fetchTodos = async () => {
    const response = await axios.get(API_URL);
    setTodos(response.data);
  };

  const addTodo = async () => {
    if (!newTitle.trim()) return alert('Title cannot be empty');
    await axios.post(API_URL, {
      title: newTitle,
      isCompleted: false
    });
    setNewTitle('');
    fetchTodos();
  };

  const toggleComplete = async (todo) => {
    await axios.put(`${API_URL}/${todo.id}`, {
      ...todo,
      isCompleted: !todo.isCompleted
    });
    fetchTodos();
  };

  const deleteTodo = async (id) => {
    await axios.delete(`${API_URL}/${id}`);
    fetchTodos();
  };

  return (
    <div style={{ padding: '20px' }}>
      <h2>📝 To-Do List</h2>
      <input
        type="text"
        placeholder="New todo title"
        value={newTitle}
        onChange={e => setNewTitle(e.target.value)}
      />
      <button onClick={addTodo}>Add</button>

      <ul>
        {todos.map(todo => (
          <li key={todo.id}>
            <span
              style={{
                textDecoration: todo.isCompleted ? 'line-through' : 'none',
                cursor: 'pointer'
              }}
              onClick={() => toggleComplete(todo)}
            >
              {todo.title}
            </span>
            <button onClick={() => deleteTodo(todo.id)} style={{ marginLeft: '10px' }}>
              ❌
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
