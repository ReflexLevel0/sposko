import React, { useState } from "react";
import axios from "../api/axiosInstance";
import { useNavigate } from "react-router-dom";
import "./LoginRegister.css";

const Register = () => {
  const [role, setRole] = useState("parent");
  const [form, setForm] = useState({
    // zajednička polja
    first_name: "",
    last_name: "",
    phone_number: "",
    email: "",
    password: "",
    // trener specifično
    date_of_birth: "",
    info: "",
  });
  const [success, setSuccess] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleRoleChange = (e) => {
    setRole(e.target.value);
    setForm({
      first_name: "",
      last_name: "",
      phone_number: "",
      email: "",
      password: "",
      date_of_birth: "",
      info: "",
    });
    setError("");
    setSuccess("");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    try {
      const endpoint =
        role === "parent" ? "/api/auth/register/parent" : "/api/auth/register/trainer";
      const payload =
        role === "parent"
          ? {
              first_name: form.first_name,
              last_name: form.last_name,
              phone_number: form.phone_number,
              email: form.email,
              password: form.password,
            }
          : {
              first_name: form.first_name,
              last_name: form.last_name,
              phone_number: form.phone_number,
              email: form.email,
              password: form.password,
              date_of_birth: form.date_of_birth,
              info: form.info,
            };
      await axios.post(endpoint, payload);
      setSuccess("Registracija uspješna! Prijavite se.");
      setTimeout(() => navigate("/login"), 1500);
    } catch (err) {
      setError("Greška prilikom registracije. Provjerite podatke.");
    }
  };

  return (
    <div className="auth-container">
      <h2>Registracija</h2>
      <div className="role-switch">
        <label>
          <input
            type="radio"
            value="parent"
            checked={role === "parent"}
            onChange={handleRoleChange}
          />
          Roditelj
        </label>
        <label>
          <input
            type="radio"
            value="trainer"
            checked={role === "trainer"}
            onChange={handleRoleChange}
          />
          Trener
        </label>
      </div>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="first_name"
          placeholder="Ime"
          value={form.first_name}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="last_name"
          placeholder="Prezime"
          value={form.last_name}
          onChange={handleChange}
          required
        />
        <input
          type="text"
          name="phone_number"
          placeholder="Telefon"
          value={form.phone_number}
          onChange={handleChange}
          required
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={form.email}
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
        {role === "trainer" && (
          <>
            <input
              type="date"
              name="date_of_birth"
              placeholder="Datum rođenja"
              value={form.date_of_birth}
              onChange={handleChange}
              required
            />
            <textarea
              name="info"
              placeholder="Kratki opis/iskustvo"
              value={form.info}
              onChange={handleChange}
              required
            />
          </>
        )}
        <button type="submit">Registriraj se</button>
        {error && <div className="auth-error">{error}</div>}
        {success && <div className="auth-success">{success}</div>}
      </form>
    </div>
  );
};

export default Register;
