/*
 * Copyright (C) 2011 The Libphonenumber Authors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace com.google.i18n.phonenumbers {

using PhoneNumberType = com.google.i18n.phonenumbers.PhoneNumberUtil.PhoneNumberType;
using PhoneNumber = com.google.i18n.phonenumbers.Phonenumber.PhoneNumber;
using NumberFormat = com.google.i18n.phonenumbers.Phonemetadata.NumberFormat;
using PhoneMetadata = com.google.i18n.phonenumbers.Phonemetadata.PhoneMetadata;
using CountryCodeSource = com.google.i18n.phonenumbers.Phonenumber.PhoneNumber.CountryCodeSource;
using MatchType = com.google.i18n.phonenumbers.PhoneNumberUtil.MatchType;
using PhoneNumberFormat = com.google.i18n.phonenumbers.PhoneNumberUtil.PhoneNumberFormat;
using PhoneMetadataCollection = com.google.i18n.phonenumbers.Phonemetadata.PhoneMetadataCollection;
using PhoneNumberDesc = com.google.i18n.phonenumbers.Phonemetadata.PhoneNumberDesc;
using Leniency = com.google.i18n.phonenumbers.PhoneNumberUtil.Leniency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.google.i18n.phonenumbers;
using Test;
using java.lang;
using java.util.logging;
using java.util;




/**
 * Unit tests for ShortNumberUtil.java
 *
 * @author Shaopeng Jia
 */
[TestClass] public class ShortNumberUtilTest : TestCase {
  private ShortNumberUtil shortUtil;

  public ShortNumberUtilTest() {
    shortUtil = new ShortNumberUtil();
  }

  [TestMethod] public void testConnectsToEmergencyNumber_US() {
    assertTrue(shortUtil.connectsToEmergencyNumber("911", RegionCode.US));
    assertTrue(shortUtil.connectsToEmergencyNumber("112", RegionCode.US));
    assertFalse(shortUtil.connectsToEmergencyNumber("999", RegionCode.US));
  }

  [TestMethod] public void testConnectsToEmergencyNumberLongNumber_US() {
    assertTrue(shortUtil.connectsToEmergencyNumber("9116666666", RegionCode.US));
    assertTrue(shortUtil.connectsToEmergencyNumber("1126666666", RegionCode.US));
    assertFalse(shortUtil.connectsToEmergencyNumber("9996666666", RegionCode.US));
  }

  [TestMethod] public void testConnectsToEmergencyNumberWithFormatting_US() {
    assertTrue(shortUtil.connectsToEmergencyNumber("9-1-1", RegionCode.US));
    assertTrue(shortUtil.connectsToEmergencyNumber("1-1-2", RegionCode.US));
    assertFalse(shortUtil.connectsToEmergencyNumber("9-9-9", RegionCode.US));
  }

  [TestMethod] public void testConnectsToEmergencyNumberWithPlusSign_US() {
    assertFalse(shortUtil.connectsToEmergencyNumber("+911", RegionCode.US));
    assertFalse(shortUtil.connectsToEmergencyNumber("\uFF0B911", RegionCode.US));
    assertFalse(shortUtil.connectsToEmergencyNumber(" +911", RegionCode.US));
    assertFalse(shortUtil.connectsToEmergencyNumber("+112", RegionCode.US));
    assertFalse(shortUtil.connectsToEmergencyNumber("+999", RegionCode.US));
  }

  [TestMethod] public void testConnectsToEmergencyNumber_BR() {
    assertTrue(shortUtil.connectsToEmergencyNumber("911", RegionCode.BR));
    assertTrue(shortUtil.connectsToEmergencyNumber("190", RegionCode.BR));
    assertFalse(shortUtil.connectsToEmergencyNumber("999", RegionCode.BR));
  }

  [TestMethod] public void testConnectsToEmergencyNumberLongNumber_BR() {
    // Brazilian emergency numbers don't work when additional digits are appended.
    assertFalse(shortUtil.connectsToEmergencyNumber("9111", RegionCode.BR));
    assertFalse(shortUtil.connectsToEmergencyNumber("1900", RegionCode.BR));
    assertFalse(shortUtil.connectsToEmergencyNumber("9996", RegionCode.BR));
  }

  [TestMethod] public void testConnectsToEmergencyNumber_AO() {
    // Angola doesn't have any metadata for emergency numbers in the test metadata.
    assertFalse(shortUtil.connectsToEmergencyNumber("911", RegionCode.AO));
    assertFalse(shortUtil.connectsToEmergencyNumber("222123456", RegionCode.AO));
    assertFalse(shortUtil.connectsToEmergencyNumber("923123456", RegionCode.AO));
  }

  [TestMethod] public void testConnectsToEmergencyNumber_ZW() {
    // Zimbabwe doesn't have any metadata in the test metadata.
    assertFalse(shortUtil.connectsToEmergencyNumber("911", RegionCode.ZW));
    assertFalse(shortUtil.connectsToEmergencyNumber("01312345", RegionCode.ZW));
    assertFalse(shortUtil.connectsToEmergencyNumber("0711234567", RegionCode.ZW));
  }

  [TestMethod] public void testIsEmergencyNumber_US() {
    assertTrue(shortUtil.isEmergencyNumber("911", RegionCode.US));
    assertTrue(shortUtil.isEmergencyNumber("112", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("999", RegionCode.US));
  }

  [TestMethod] public void testIsEmergencyNumberLongNumber_US() {
    assertFalse(shortUtil.isEmergencyNumber("9116666666", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("1126666666", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("9996666666", RegionCode.US));
  }

  [TestMethod] public void testIsEmergencyNumberWithFormatting_US() {
    assertTrue(shortUtil.isEmergencyNumber("9-1-1", RegionCode.US));
    assertTrue(shortUtil.isEmergencyNumber("*911", RegionCode.US));
    assertTrue(shortUtil.isEmergencyNumber("1-1-2", RegionCode.US));
    assertTrue(shortUtil.isEmergencyNumber("*112", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("9-9-9", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("*999", RegionCode.US));
  }

  [TestMethod] public void testIsEmergencyNumberWithPlusSign_US() {
    assertFalse(shortUtil.isEmergencyNumber("+911", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("\uFF0B911", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber(" +911", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("+112", RegionCode.US));
    assertFalse(shortUtil.isEmergencyNumber("+999", RegionCode.US));
  }

  [TestMethod] public void testIsEmergencyNumber_BR() {
    assertTrue(shortUtil.isEmergencyNumber("911", RegionCode.BR));
    assertTrue(shortUtil.isEmergencyNumber("190", RegionCode.BR));
    assertFalse(shortUtil.isEmergencyNumber("999", RegionCode.BR));
  }

  [TestMethod] public void testIsEmergencyNumberLongNumber_BR() {
    assertFalse(shortUtil.isEmergencyNumber("9111", RegionCode.BR));
    assertFalse(shortUtil.isEmergencyNumber("1900", RegionCode.BR));
    assertFalse(shortUtil.isEmergencyNumber("9996", RegionCode.BR));
  }

  [TestMethod] public void testIsEmergencyNumber_AO() {
    // Angola doesn't have any metadata for emergency numbers in the test metadata.
    assertFalse(shortUtil.isEmergencyNumber("911", RegionCode.AO));
    assertFalse(shortUtil.isEmergencyNumber("222123456", RegionCode.AO));
    assertFalse(shortUtil.isEmergencyNumber("923123456", RegionCode.AO));
  }

  [TestMethod] public void testIsEmergencyNumber_ZW() {
    // Zimbabwe doesn't have any metadata in the test metadata.
    assertFalse(shortUtil.isEmergencyNumber("911", RegionCode.ZW));
    assertFalse(shortUtil.isEmergencyNumber("01312345", RegionCode.ZW));
    assertFalse(shortUtil.isEmergencyNumber("0711234567", RegionCode.ZW));
  }
}

}