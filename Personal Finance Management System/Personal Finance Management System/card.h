#include "personalData.h"
#include "dateOfExpiry.h"


class card
{
protected:
	uint16_t* cardNumber{};
	uint16_t* CVV{};
public:
	personalData* ownerData{};
	dateOfExpiry* dateExpiry;

	card(std::string&, std::string&, std::string&, uint16_t&, uint16_t&, uint16_t&, uint16_t&, uint16_t&, uint16_t&, uint16_t&);

	uint16_t getCardNumber() const;
	uint16_t getCVV() const;

	~card();
};