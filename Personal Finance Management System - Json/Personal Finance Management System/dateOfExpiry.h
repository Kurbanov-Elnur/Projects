#include <iostream>
#include "nlohmann/json.hpp"

using json = nlohmann::json;
#pragma once

class dateOfExpiry
{
private:
	uint16_t* monthOfExpiry;
	uint16_t* yearOFExpiry;
public:
	dateOfExpiry(uint16_t&, uint16_t&);

	dateOfExpiry();

	dateOfExpiry(const dateOfExpiry& _other);

	friend std::ostream& operator << (std::ostream& os, dateOfExpiry _dateOfExpiry)
	{
		os << "Date of Expiry: " << *_dateOfExpiry.monthOfExpiry << '.' << *_dateOfExpiry.yearOFExpiry << std::endl;
		return os;
	}

	friend json& operator << (json& jsonData, dateOfExpiry _dateOfExpiry)
	{
		jsonData["monthOfExpiry"] = *_dateOfExpiry.monthOfExpiry;
		jsonData["yearOFExpiry"] = *_dateOfExpiry.yearOFExpiry;

		return jsonData;
	}

	friend json& operator >>(json& jsonData, dateOfExpiry& _dateOfExpiry)
	{
		*_dateOfExpiry.monthOfExpiry = jsonData["monthOfExpiry"];
		*_dateOfExpiry.yearOFExpiry = jsonData["yearOFExpiry"];
		return jsonData;
	}

	friend std::istream& operator>>(std::istream& is, dateOfExpiry& _dateOfExpiry)
	{
		is >> *_dateOfExpiry.monthOfExpiry >> *_dateOfExpiry.yearOFExpiry;
		return is;
	}

	uint16_t getMonthOfExpiry() const;
	uint16_t getYearOfExpiry() const;

	~dateOfExpiry();
};

