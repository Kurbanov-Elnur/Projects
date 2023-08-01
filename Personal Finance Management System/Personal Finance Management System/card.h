#include "dateOfExpiry.h"
#include "personalData.h"
#pragma once

class card
{
protected:
	std::string cardNumber{};
	uint16_t* CVV{};
	float* balance{};
public:
	personalData* ownerData{};
	dateOfExpiry* dateExpiry{};

	card(personalData&, std::string&, uint16_t&, float&, dateOfExpiry&);

	void setBalance(float newBalance);

	std::string getCardNumber() const;
	uint16_t getCVV() const;
	uint16_t getBalance() const;
};