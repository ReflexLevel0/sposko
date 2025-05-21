import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import Navbar from "./components/Navbar";
import HomePage from "./pages/HomePage";
import Login from "./pages/Login";
import Register from "./pages/Register";
import AvailableGroups from "./pages/AvailableGroups";
import MyTrainings from "./pages/MyTrainings";
import Profile from "./pages/Profile";
import CreateGroup from "./pages/CreateGroup";
import ApproveTrainers from "./pages/ApproveTrainers";
import ProtectedRoute from "./components/ProtectedRoute";
import "./App.css";

function App() {
  return (
    <AuthProvider>
      <Router>
        <Navbar />
        <div className="main-content">
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/groups" element={<AvailableGroups />} />
            <Route path="/my-trainings" 
              element={
                <ProtectedRoute allowedRoles={["parent", "trainer"]}>
                  <MyTrainings />
                </ProtectedRoute>
              } 
            />
            <Route path="/profile" 
              element={
                <ProtectedRoute allowedRoles={["parent", "trainer"]}>
                  <Profile />
                </ProtectedRoute>
              } 
            />
            <Route path="/create-group" 
              element={
                <ProtectedRoute allowedRoles={["trainer"]}>
                  <CreateGroup />
                </ProtectedRoute>
              } 
            />
            <Route path="/approve-trainers" 
              element={
                <ProtectedRoute allowedRoles={["admin"]}>
                  <ApproveTrainers />
                </ProtectedRoute>
              } 
            />
          </Routes>
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;
