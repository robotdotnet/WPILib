// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

// THIS FILE WAS AUTO-GENERATED BY ./ntcore/generate_topics.py. DO NOT MODIFY

using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

/// <summary>
/// NetworkTables BooleanArray topic.
/// </summary>
public class BooleanArrayTopic : Topic
{
    /// <summary>
    /// The default type string for this topic type
    /// </summary>
    public static string kTypeString => "boolean[]";
    /// <summary>
    /// The default type string for this topic type in a UTF8 Span
    /// </summary>
    public static ReadOnlySpan<byte> kTypeStringUtf8 => "boolean[]"u8;

    /// <summary>
    /// Constructs a typed topic from a generic topic.
    /// </summary>
    /// <param name="topic">typed topic</param>
    public BooleanArrayTopic(Topic topic) : base(topic.Instance, topic.Handle) { }

    /// <summary>
    /// Constructor; use NetworkTableInstance.GetBooleanArrayTopic() instead.
    /// </summary>
    /// <param name="inst">Instance</param>
    /// <param name="handle">Native handle</param>
    public BooleanArrayTopic(NetworkTableInstance inst, NtTopic handle) : base(inst, handle) { }


    /// <summary>
    /// Create a new subscriver to the topic.
    /// </summary>
    /// <remarks>
    /// The subscriber is only active as long as the returned object is not closed.
    ///
    /// Subscribers that do not match the published data type do not return any
    /// values. To determine if the data type matches, use the appropriate Topic
    /// functions.
    /// </remarks>
    /// <param name="defaultValue">
    /// default value used when a default is not provided to a getter function
    /// </param>
    /// <param name="options">subscribe options</param>
    /// <returns>subscriber</returns>
    public IBooleanArraySubscriber Subscribe(
        bool[] defaultValue,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtSubscriber>(
            this,
            NtCore.Subscribe(
                Handle, NetworkTableType.BooleanArray,
                "boolean[]"u8, options),
            defaultValue ?? []);
    }

    /// <summary>
    /// Create a new subscriber to the topic, with the specified type string.
    /// </summary>
    /// <remarks>
    /// The subscriber is only active as long as the returned object is not closed.
    ///
    /// Subscribers that do not match the published data type do not return any
    /// values. To determine if the data type matches, use the appropriate Topic
    /// functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="defaultValue">
    /// default value used when a default is not provided to a getter function
    /// </param>
    /// <param name="options">subscribe options</param>
    /// <returns>subscriber</returns>
    public IBooleanArraySubscriber SubscribeEx(
        string typeString,
        bool[] defaultValue,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtSubscriber>(
            this,
            NtCore.Subscribe(
                Handle, NetworkTableType.BooleanArray,
                typeString, options),
            defaultValue ?? []);
    }

    /// <summary>
    /// Create a new subscriber to the topic, with the specified type string.
    /// </summary>
    /// <remarks>
    /// The subscriber is only active as long as the returned object is not closed.
    ///
    /// Subscribers that do not match the published data type do not return any
    /// values. To determine if the data type matches, use the appropriate Topic
    /// functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="defaultValue">
    /// default value used when a default is not provided to a getter function
    /// </param>
    /// <param name="options">subscribe options</param>
    /// <returns>subscriber</returns>
    public IBooleanArraySubscriber SubscribeEx(
        ReadOnlySpan<byte> typeString,
        bool[] defaultValue,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtSubscriber>(
            this,
            NtCore.Subscribe(
                Handle, NetworkTableType.BooleanArray,
                typeString, options),
            defaultValue ?? []);
    }

    /// <summary>
    /// Create a new publisher to the topic.
    /// </summary>
    /// <remarks>
    /// The publisher is only active as long as the returned object is not closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="options">publish options</param>
    /// <returns>publisher</returns>
    public IBooleanArrayPublisher Publish(
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtPublisher>(
            this,
            NtCore.Publish(
                Handle, NetworkTableType.BooleanArray,
                "boolean[]"u8, options),
            []);
    }

    /// <summary>
    /// Create a new publisher to the topic, with type string and initial properties.
    /// </summary>
    /// <remarks>
    /// The publisher is only active as long as the returned object is not closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="properties">JSON properties</param>
    /// <param name="options">publish options</param>
    /// <returns>publisher</returns>
    public IBooleanArrayPublisher PublishEx(
        string typeString, string properties,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtPublisher>(
            this,
            NtCore.PublishEx(
                Handle, NetworkTableType.BooleanArray,
                typeString, properties, options),
            []);
    }

    /// <summary>
    /// Create a new publisher to the topic, with type string and initial properties.
    /// </summary>
    /// <remarks>
    /// The publisher is only active as long as the returned object is not closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="properties">JSON properties</param>
    /// <param name="options">publish options</param>
    /// <returns>publisher</returns>
    public IBooleanArrayPublisher PublishEx(
        ReadOnlySpan<byte> typeString,
        string properties,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtPublisher>(
            this,
            NtCore.PublishEx(
                Handle, NetworkTableType.BooleanArray,
                typeString, properties, options),
            []);
    }

    /// <summary>
    /// Create a new publisher to the topic, with type string and initial properties.
    /// </summary>
    /// <remarks>
    /// The publisher is only active as long as the returned object is not closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="properties">JSON properties</param>
    /// <param name="options">publish options</param>
    /// <returns>publisher</returns>
    public IBooleanArrayPublisher PublishEx(
        string typeString,
        ReadOnlySpan<byte> properties,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtPublisher>(
            this,
            NtCore.PublishEx(
                Handle, NetworkTableType.BooleanArray,
                typeString, properties, options),
            []);
    }

    /// <summary>
    /// Create a new publisher to the topic, with type string and initial properties.
    /// </summary>
    /// <remarks>
    /// The publisher is only active as long as the returned object is not closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="properties">JSON properties</param>
    /// <param name="options">publish options</param>
    /// <returns>publisher</returns>
    public IBooleanArrayPublisher PublishEx(
        ReadOnlySpan<byte> typeString,
        ReadOnlySpan<byte> properties,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtPublisher>(
            this,
            NtCore.PublishEx(
                Handle, NetworkTableType.BooleanArray,
                typeString, properties, options),
            []);
    }

    /// <summary>
    /// Create a new entry for the topic.
    /// </summary>
    /// <remarks>
    /// Entries act as a combination of a subscriber and a weak publisher. The
    /// subscriber is active as long as the entry is not closed. The publisher is
    /// created when the entry is first written to, and remains active until either
    /// Unpublish() is called or the entry is closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="defaultValue">
    /// default value used when a default is not provided to a getter function
    /// </param>
    /// <param name="options">publish and/or subscribe options</param>
    /// <returns>entry</returns>
    public IBooleanArrayEntry GetEntry(
        bool[] defaultValue,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtEntry>(
            this,
            NtCore.GetEntry(
                Handle, NetworkTableType.BooleanArray,
                "boolean[]"u8, options),
            defaultValue ?? []);
    }

    /// <summary>
    /// Create a new entry for the topic, with the specified type string.
    /// </summary>
    /// <remarks>
    /// Entries act as a combination of a subscriber and a weak publisher. The
    /// subscriber is active as long as the entry is not closed. The publisher is
    /// created when the entry is first written to, and remains active until either
    /// Unpublish() is called or the entry is closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="defaultValue">
    /// default value used when a default is not provided to a getter function
    /// </param>
    /// <param name="options">publish and/or subscribe options</param>
    /// <returns>entry</returns>
    public IBooleanArrayEntry GetEntryEx(
        string typeString,
        bool[] defaultValue,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtEntry>(
            this,
            NtCore.GetEntry(
                Handle, NetworkTableType.BooleanArray,
                typeString, options),
            defaultValue ?? []);
    }

    /// <summary>
    /// Create a new entry for the topic, with the specified type string.
    /// </summary>
    /// <remarks>
    /// Entries act as a combination of a subscriber and a weak publisher. The
    /// subscriber is active as long as the entry is not closed. The publisher is
    /// created when the entry is first written to, and remains active until either
    /// Unpublish() is called or the entry is closed.
    ///
    /// It is not possible to publish two different data types to the same topic.
    /// Conflicts between publishers are typically resolved by the server on a
    /// first-come, first-served basis. Any published values that do not match
    /// the topic's data type are dropped (ignored). To determine if the data
    /// type matches, use tha appropriate Topic functions.
    /// </remarks>
    /// <param name="typeString">type string</param>
    /// <param name="defaultValue">
    /// default value used when a default is not provided to a getter function
    /// </param>
    /// <param name="options">publish and/or subscribe options</param>
    /// <returns>entry</returns>
    public IBooleanArrayEntry GetEntryEx(
        ReadOnlySpan<byte> typeString,
        bool[] defaultValue,
        PubSubOptions options = default)
    {
        return new BooleanArrayEntryImpl<NtEntry>(
            this,
            NtCore.GetEntry(
                Handle, NetworkTableType.BooleanArray,
                typeString, options),
            defaultValue ?? []);
    }

}
