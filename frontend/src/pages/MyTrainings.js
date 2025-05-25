import React, { useContext, useEffect, useState } from "react";
import axios from "../api/axiosInstance";
import { AuthContext } from "../context/AuthContext";
import "./MyTrainings.css";

const MyTrainings = () => {
  const { user } = useContext(AuthContext);

  // Za roditelja
  const [children, setChildren] = useState([]);
  const [childGroups, setChildGroups] = useState({});
  const [trainings, setTrainings] = useState({});
  // Za trenera
  const [groups, setGroups] = useState([]);
  const [groupTrainings, setGroupTrainings] = useState({});
  const [showAddTraining, setShowAddTraining] = useState(null);
  const [newTraining, setNewTraining] = useState({ date: "", time: "", duration: "" });
  const [success, setSuccess] = useState("");
  const [error, setError] = useState("");

  // --- Roditelj: dohvat djece i njihovih grupa ---
  useEffect(() => {
    if (user && user.role === "parent") {
      axios.get(`/api/children?parent_id=${user.id}`).then(res => {
        setChildren(res.data);
        // Za svako dijete, dohvat grupa
        res.data.forEach(child => {
          axios.get(`/api/child_group?child_id=${child.id}`).then(res2 => {
            setChildGroups(prev => ({ ...prev, [child.id]: res2.data }));
            // Za svaku grupu, dohvat treninga
            res2.data.forEach(group => {
              axios.get(`/api/sport_trainings?group_id=${group.group_id}`).then(res3 => {
                setTrainings(prev => ({
                  ...prev,
                  [`${child.id}-${group.group_id}`]: res3.data,
                }));
              });
            });
          });
        });
      });
    }
  }, [user]);

  // --- Trener: dohvat grupa i treninga ---
  useEffect(() => {
    if (user && user.role === "trainer") {
      axios.get(`/api/sportgroup?trainerid=${user.id}`).then(res => {
        let groups = []
        res.data.forEach(group => {
          axios.get(`/api/sport/${group.sportId}`).then(sportRes => {
            group.sportname = sportRes.data.name
          })
          groups.push(group)
        })
        setGroups(groups);

        res.data.forEach(group => {
          axios.get(`/api/sporttraining?groupid=${group.id}`).then(res2 => {
            let trainings = []
            res2.data.forEach(training => {
              trainings.push({
                startDate: new Date(training.startDate),
                startTime: new Date(training.startTime),
                duration: training.duration
              })
            })
            setGroupTrainings(prev => ({
              ...prev,
              [group.id]: trainings,
            }));
          });
        });
      });
    }
  }, [user]);

  // --- Dodavanje treninga (trener) ---
  const handleAddTraining = async (groupId) => {
    setError("");
    setSuccess("");
    if (!newTraining.date || !newTraining.time || !newTraining.duration) {
      setError("Popunite sva polja za novi trening.");
      return;
    }
    try {
      console.log(newTraining.date.toString("yyyy-MM-dd") + "T" + newTraining.time.toString("HH:mm"))
      await axios.post("/api/sporttraining", {
        groupid: groupId,
        startdate: newTraining.date.toString("yyyy-MM-dd"),
        starttime: newTraining.date.toString("yyyy-MM-dd") + "T" + newTraining.time.toString("HH:mm") + ":00",
        duration: "0." + parseInt(newTraining.duration / 60).toString() + ":" + newTraining.duration % 60 + ":00.0000"
      });
      setSuccess("Trening uspješno dodan!");
      setShowAddTraining(null);
      setNewTraining({ date: "", time: "", duration: "" });
      // Osvježi treninge za grupu
      axios.get(`/api/sporttraining?groupid=${group.id}`).then(res2 => {
            let trainings = []
            res2.data.forEach(training => {
              trainings.push({
                startDate: new Date(training.startDate),
                startTime: new Date(training.startTime),
                duration: training.duration
              })
            })
            setGroupTrainings(prev => ({
              ...prev,
              [group.id]: trainings,
            }));
          });
    } catch (err) {
      setError("Greška pri dodavanju treninga.");
    }
  };

  if (!user) return <div className="mytrainings-info">Prijavite se kako biste vidjeli svoje treninge.</div>;

  return (
    <div className="mytrainings-container">
      <h2>Moji treninzi</h2>
      {user.role === "parent" && (
        <>
          {children.length === 0 ? (
            <div>Nemate prijavljene djece.</div>
          ) : (
            children.map(child => (
              <div key={child.id} className="child-section">
                <h3>{child.first_name} {child.last_name}</h3>
                {childGroups[child.id] && childGroups[child.id].length > 0 ? (
                  childGroups[child.id].map(group => (
                    <div key={group.group_id} className="group-section">
                      <div className="group-title">
                        Grupa: <b>{group.group_name}</b>
                      </div>
                      <div className="trainings-list">
                        {trainings[`${child.id}-${group.group_id}`] && trainings[`${child.id}-${group.group_id}`].length > 0 ? (
                          trainings[`${child.id}-${group.group_id}`].map(tr => (
                            <div key={tr.id} className="training-card">
                              <div>Datum: {tr.date}</div>
                              <div>Vrijeme: {tr.time}</div>
                              <div>Lokacija: {tr.location}</div>
                            </div>
                          ))
                        ) : (
                          <div className="no-trainings">Nema treninga za ovu grupu.</div>
                        )}
                      </div>
                    </div>
                  ))
                ) : (
                  <div className="no-groups">Dijete nije prijavljeno ni u jednu grupu.</div>
                )}
              </div>
            ))
          )}
        </>
      )}

      {user.role === "trainer" && (
        <>
          {groups.length === 0 ? (
            <div>Nemate kreiranih grupa.</div>
          ) : (
            groups.map(group => (
              <div key={group.id} className="group-section">
                <div className="group-title">
                  Grupa: <b>{group.name}</b> ({group.sportname}) ({group.minAge}-{group.maxAge} godina) (0/{group.maxMembers} članova)
                </div>
                <button
                  className="add-training-btn"
                  onClick={() => setShowAddTraining(group.id)}
                >
                  Dodaj novi trening
                </button>
                {showAddTraining === group.id && (
                  <div className="add-training-form">
                    <input
                      type="date"
                      value={newTraining.date}
                      onChange={e => setNewTraining({ ...newTraining, date: e.target.value })}
                      required
                    />
                    <input
                      type="time"
                      value={newTraining.time}
                      onChange={e => setNewTraining({ ...newTraining, time: e.target.value })}
                      required
                    />
                    <input
                      type="text"
                      placeholder="Trajanje (u minutama)"
                      value={newTraining.duration}
                      onChange={e => setNewTraining({ ...newTraining, duration: e.target.value })}
                      required
                    />
                    <button onClick={() => handleAddTraining(group.id)}>Spremi</button>
                    <button onClick={() => setShowAddTraining(null)} type="button">Odustani</button>
                  </div>
                )}
                <div className="trainings-list">
                  {groupTrainings[group.id] && groupTrainings[group.id].length > 0 ? (
                    groupTrainings[group.id].map(tr => (
                      <div key={tr.id} className="training-card">
                        <div>Datum: {tr.startDate.getDate()}.{tr.startDate.getMonth()+1}.{tr.startDate.getFullYear()}.</div>
                        <div>Vrijeme: {tr.startTime.getUTCHours()}:{tr.startTime.getUTCMinutes()}</div>
                        <div>Trajanje: {tr.duration.split(":")[0]}:{tr.duration.split(":")[1]}</div>
                      </div>
                    ))
                  ) : (
                    <div className="no-trainings">Nema treninga za ovu grupu.</div>
                  )}
                </div>
              </div>
            ))
          )}
        </>
      )}
      {error && <div className="mytrainings-error">{error}</div>}
      {success && <div className="mytrainings-success">{success}</div>}
    </div>
  );
};

export default MyTrainings;
