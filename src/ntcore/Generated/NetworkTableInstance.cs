﻿// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

// THIS FILE WAS AUTO-GENERATED BY ./ntcore/generate_topics.py. DO NOT MODIFY

using NetworkTables.Handles;

namespace NetworkTables;

/**
 * NetworkTables Instance.
 *
 * <p>Instances are completely independent from each other. Table operations on one instance will
 * not be visible to other instances unless the instances are connected via the network. The main
 * limitation on instances is that you cannot have two servers on the same network port. The main
 * utility of instances is for unit testing, but they can also enable one program to connect to two
 * different NetworkTables networks.
 *
 * <p>The global "default" instance (as returned by {@link #GetDefault()}) is always available, and
 * is intended for the common case when there is only a single NetworkTables instance being used in
 * the program.
 *
 * <p>Additional instances can be created with the {@link #create()} function. A reference must be
 * kept to the NetworkTableInstance returned by this function to keep it from being garbage
 * collected.
 */
public sealed class NetworkTableInstance
{
    public NtInst Handle { get; }
}