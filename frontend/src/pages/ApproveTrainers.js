import React, { useEffect, useState, useContext } from "react";
import axios from "../api/axiosInstance";
import { AuthContext } from "../context/AuthContext";
import "./ApproveTrainers.css";

const ApproveTrainers = () => {
  const { user } = useContext(AuthContext);
  const [pending, setPending] = useState([]);
  const [success, setSuccess] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    if (user && user.role === "admin") {
      axios
        .get("/api/trainers?verified=false")
        .then((res) => setPending(res.data))
        .catch(() => setPending([]));
    }
  }, [user]);

  const handleApprove = async (trainerId) => {
    setError("");
    setSuccess("");
    try {
      await axios.patch(`/api/trainers/${trainerId}/approve`);
      setSuccess("Trener je odobren!");
      setPending((prev) => prev.filter((t) => t.id !== trainerId));
    } catch {
      setError("Greška pri odobravanju trenera.");
    }
  };

  if (!user || user.role !== "admin") {
    return <div className="approve-info">Samo admin ima pristup ovoj stranici.</div>;
  }

  return (
    <div className="approve-container">
      <h2>Odobri prijavu trenera</h2>
      {pending.length === 0 ? (
        <div>Nema trenera na čekanju za odobrenje.</div>
      ) : (
        <table className="approve-table">
          <thead>
            <tr>
              <th>Ime</th>
              <th>Prezime</th>
              <th>Email</th>
              <th>Telefon</th>
              <th>Datum rođenja</th>
              <th>Info</th>
              <th>Akcija</th>
            </tr>
          </thead>
          <tbody>
            {pending.map((trainer) => (
              <tr key={trainer.id}>
                <td>{trainer.first_name}</td>
                <td>{trainer.last_name}</td>
                <td>{trainer.email}</td>
                <td>{trainer.phone_number}</td>
                <td>{trainer.date_of_birth}</td>
                <td>{trainer.info}</td>
                <td>
                  <button onClick={() => handleApprove(trainer.id)}>
                    Odobri
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
      {error && <div className="approve-error">{error}</div>}
      {success && <div className="approve-success">{success}</div>}
    </div>
  );
};

export default ApproveTrainers;
