import React, { useState, useContext } from "react";
import axios from "../api/axiosInstance";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import "./LoginRegister.css";

const Login = () => {
  const [form, setForm] = useState({ username: "", password: "" });
  const [error, setError] = useState("");
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    try {
      const res = await axios.post("/api/auth/login", form);
      // Pretpostavljamo da backend vraća { user: {...}, token: "..." }
      login(res.data.user, res.data.token);
      navigate("/");
    } catch (err) {
      setError("Neispravno korisničko ime ili lozinka.");
    }
  };

  return (
    <div className="auth-container">
      <h2>Prijava</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="username"
          placeholder="Korisničko ime ili email"
          value={form.username}
          onChange={handleChange}
          required
        />
        <input
          type="password"
          name="password"
          placeholder="Lozinka"
          value={form.password}
          onChange={handleChange}
          required
        />
        <button type="submit">Prijavi se</button>
        {error && <div className="auth-error">{error}</div>}
      </form>
    </div>
  );
};

export default Login;
